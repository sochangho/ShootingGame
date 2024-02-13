using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;





public partial class  Enemy : Character
{
    public enum EnemyState
    {
        Idle,

        Trace,

        Attack

    }

    private Player player;

    
    public Vector3 Direction { get; private set; }

    private ObjectPooling objectPooling;

    private EnemyState enemyState;

    private NavMeshAgent navMeshAgent;   

    #region SerializedField
    [SerializeField]
    private float detectDistance;

    [SerializeField]
    private float attackDistacne;
    #endregion

    private Coroutine corutineAI;

    override public void Created()
    { 
        base.Created();

    }

    override public void Destroyed() { base.Destroyed(); }

    override public void Active() 
    { 
        base.Active();

        Debug.Log("<color=red> Monster Active </color>");

        isDie = false;

        enemyState = EnemyState.Idle;

        player = GameScene.Instance.GameRoopController.player;

        if (characterInfo != null)
        {
            characterInfo.RefreshInfo(); 
        }

        corutineAI = StartCoroutine(EnemyAIUpdata());
             
    }


    override public void InActive() { base.InActive(); }


    protected override void AniWalk(bool isWalk)
    {
        CharacterAnimator.SetBool("IsWalk", isWalk);
        CharacterAnimator.SetBool("IsAttack", !isWalk);
    }

    protected override void AniAttack()
    {
        if(CharacterAnimator == null)
        {
            return;
        }
        CharacterAnimator.SetBool("IsWalk", false);
        CharacterAnimator.SetBool("IsAttack", true);
    }

    public override void CharacterCreated(int id)
    {
        var d = InfoManager.Instance.TableEnemys.GetData(id);
        
        if (characterInfo == null)
        {
            characterInfo = new EnemyInfo();
            characterInfo.SetInfo(d);
        }

        if (baseAttack == null)
        {
            baseAttack = ClassFactory.GetClassInstanceAIAttack(((EnemyInfo)characterInfo).AIAttackType);
            var at = (AICharacterAttack)baseAttack;
            at.Set(this);            
        }

        if (baseAttacked == null)
        {
            baseAttacked = new BaseAttacked(characterInfo);
            baseAttacked.ResisterObserver(this);
        }

        if(navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            navMeshAgent.speed = characterInfo.Speed;

        }


        ObserversResister();
    }

    public override void CharacterUpdate()
    {
        // Direction = 방향가져오기

      
    }



    public override Vector3 GetAimDirection()
    {
        return Vector3.zero;
    }



    public override Vector3 GetShotStartPos()
    {
        var pos =  this.gameObject.transform.position;

        pos.y += 1;

        return pos;
    }



    public override void Die()
    {
        

        isDie = true;
        CharacterAnimator.SetTrigger("Die");
        
    }

    public override void DieAnimationEvent()
    {
        

        if (objectPooling == null)
        {
            objectPooling = GetComponent<ObjectPooling>();
        }



        CharacterAnimator.SetBool("IsWalk", false);
        CharacterAnimator.SetBool("IsAttack", false);

        objectPooling.Destroy();
       
    }


    public override void AttackAnimationEvent()
    {
        if(baseAttack != null)
        {
            baseAttack.Attack(GetAimDirection(), GetShotStartPos(), player, this);
        }
    }

    public float DetectDistance()
    {
        return detectDistance;
    }

    public float AttackDistance()
    {
        return attackDistacne;
    }


    #region Unity Event

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR

        Gizmos.color = Color.red;
        UtillGizmo.RenderSphere(this.transform.position, attackDistacne);

        Gizmos.color = Color.blue;
        UtillGizmo.RenderSphere(this.transform.position, detectDistance);

#endif 
    }


    #endregion

}


public partial class Enemy : Character
{

    public event System.Action<Character,Character> EventIdle;

    public event System.Action<Character,Character> EventTrace;

    public event System.Action<Character,Character> EventAttack;

    public event System.Action<Character, Character> EventCollider;


    private AIDelay aIDelay;

    private bool isCollider = false;

    public void SetAIDelay(AIDelay aIDelay)
    {
        this.aIDelay = aIDelay;
    }




    IEnumerator EnemyAIUpdata()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f); // 0.1f 마다 업데이트 , 성능 상태에 따라 더 늘릴 수 있음

        while (!isDie)
        {
            if (enemyState == EnemyState.Idle)
            {
                StateIdle();

                
            }
            else if (enemyState == EnemyState.Trace)
            {
                StateTrace();

            }
            else  // 공격 
            {
                StateAttack();
                
            }
      
            yield return null;
        }

    }


    public void StateIdle()
    {

        if (EventIdle != null)
        {
            EventIdle.Invoke(player , this);
            return;
        }


        if (UtillMath.TwoPointClosed(this.transform.position, player.transform.position, detectDistance)) //this - player < distance -> true
        {
            enemyState = EnemyState.Trace;
        }

        CharacterAnimator.SetBool("IsWalk", false);
        CharacterAnimator.SetBool("IsAttack", false);

    }

    public void StateTrace()
    {
        if(EventTrace != null)
        {
            EventTrace.Invoke(player, this);
            return;
        }

        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }


        if (!navMeshAgent.enabled)
        {
            navMeshAgent.enabled = true;
        }


        if (UtillMath.TwoPointClosed(this.transform.position, player.transform.position, attackDistacne))
        {
            enemyState = EnemyState.Attack;
        }
        else
        {
            enemyState = EnemyState.Idle;
        }


        if (!navMeshAgent.pathPending)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }


        AniWalk(true);
    }

    public void StateAttack()
    {
        if (EventAttack != null)
        {
            EventAttack.Invoke(player, this);
            
            return;
        }

        if (navMeshAgent.enabled)
        {
            navMeshAgent.enabled = false;
        }

        if (!UtillMath.TwoPointClosed(this.transform.position, player.transform.position, attackDistacne))
        {
            enemyState = EnemyState.Trace;
        }

        

        Attack();
    }

    public void AIRefresh()
    {
        StartCoroutine(RefreshAI());
    }

    IEnumerator RefreshAI()
    {
        if (corutineAI != null)
        {
            StopCoroutine(corutineAI);
        }

        yield return new WaitForSeconds(2f);


        enemyState = EnemyState.Idle;
        corutineAI =  StartCoroutine(EnemyAIUpdata());
    }



    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();

        

        if (p != null && !isCollider)
        {
            
            isCollider = true;
            EventCollider?.Invoke(player, this);

            var pPos = p.transform.position;

            pPos.y += 1f;

            GameScene.Instance.ObjectPoolManager
                         .UseObject(GameObjectType.Effect, 3002, pPos);
        }

    }


    private void OnTriggerExit(Collider other)
    {

        Player p = other.GetComponent<Player>();

        if (p != null && isCollider)
        {
            isCollider = false;
            
        }

    }

}