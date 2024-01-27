using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Character : BaseObject , IObserver
{

    [SerializeField]
    private CharacterStateUI characterStateUI;

    public string CharacterName { get; private set; }

    public Animator CharacterAnimator;

    public CharacterInfo characterInfo;
    public BaseAttack baseAttack { get; protected set; }
    public BaseAttacked baseAttacked { get; protected set; }


    
    protected bool isDie = false;


    #region Override & Virtual
    override public void Created()
    {
        CharacterName = this.gameObject.name.Replace("(Clone)","");

        Debug.Log($"{CharacterName}  생성");
        
    }

    override public void Destroyed()
    {
        Debug.Log($"{CharacterName}  파괴");

    }
    
    override public void Active(){

        Debug.Log($"{CharacterName}  활성");

    }

    override public void InActive(){

        Debug.Log($"{CharacterName}  비활성");

    }

    virtual protected void AniWalk(bool isWalk)
    {
        CharacterAnimator.SetBool("IsWalk",isWalk);
    }

    virtual protected void AniAttack()
    {
        if(CharacterAnimator == null)
        {
            return;
        }
        CharacterAnimator.SetTrigger("Attack");
    }

    virtual public void Attack()
    {
        AniAttack();
    }

    virtual public void Attacked(float attack)
    {

        if(baseAttacked == null)
        {
            baseAttacked = new BaseAttacked(characterInfo);
        }

        baseAttacked.Attacked(attack);
    }


    virtual public void Walk(bool isWalk)
    {
        AniWalk(isWalk);
    }

    

    #endregion

    private void Update()
    {
       CharacterUpdate();
    }



    #region abstract

    abstract public void CharacterCreated(int id);

    abstract public void CharacterUpdate();

    abstract public void Die();

    abstract public Vector3 GetAimDirection();

    abstract public Vector3 GetShotStartPos();

    #endregion


    virtual public void AttackAnimationEvent()
    {
        if (baseAttack != null)
        {
            baseAttack.Attack(GetAimDirection(), GetShotStartPos());
        }
    }

    virtual public void DieAnimationEvent() { }

    virtual public void UpdataData(object data)
    {
        if(characterInfo.CurrHp == 0)
        {
            Die();
        }

    }

    public void ObserversResister()
    {
        characterStateUI.CharacterAttackedObserverResister(this);

        characterStateUI.CharacterRefeshObserverResister(this);
    }


}


