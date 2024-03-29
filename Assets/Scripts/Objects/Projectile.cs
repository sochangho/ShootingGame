using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseObject
{
    private ObjectPooling objectPooling;

    [SerializeField]
    protected float attack;

    protected float duration;

    protected float speed;

    protected Coroutine coroutine;

    private GameObjectType launchSubjectType;

    public int FlashId { get; set; }

    public int HitId { get; set; }


    override public void Created()
    {
        GameObjectType = GameObjectType.Projectile; // �߻�ü

    }
    override public void Destroyed() { }
    override public void Active() { }
    override public void InActive() { }


    virtual public void Fire(CharacterInfo characterInfo, Vector3 direction)
    {
        duration = characterInfo.Shot_Duration;
        speed = characterInfo.Shot_Speed;
        attack = characterInfo.Attack + characterInfo.Attack * attack / 100;

        if (characterInfo is PlayerInfo)
        {
            launchSubjectType = GameObjectType.Player;
        }
        else if (characterInfo is EnemyInfo)
        {
            launchSubjectType = GameObjectType.Enemy;
        }


        GameScene.Instance.ObjectPoolManager.UseObject(GameObjectType.Effect, FlashId, this.transform.position);

        coroutine = StartCoroutine(FireRoutin(direction));

    }

    virtual public void Attack(Collider other)
    {
        if ((launchSubjectType == GameObjectType.Enemy && other.gameObject.tag.Equals("Player")) ||
            (launchSubjectType == GameObjectType.Player && other.gameObject.tag.Equals("Enemy"))
            )
        {

            StopCoroutine(coroutine);

          
            UnUseProjectile();

            other.GetComponent<Character>().Attacked(attack);
            
        }

        
    }


    public void OnTriggerEnter(Collider other)
    {
       
        Attack(other);
    }

    public GameObjectType GetSubjectTypeProjectile()
    {
        return launchSubjectType;
    }
    public void UnUseProjectile()
    {
        if (objectPooling == null)
        {
            objectPooling = GetComponent<ObjectPooling>();
        }

        GameScene.Instance.ObjectPoolManager.UnUseObject(objectPooling);
        GameScene.Instance.ObjectPoolManager.UseObject(GameObjectType.Effect, HitId, this.transform.position);

    }

    IEnumerator FireRoutin(Vector3 direction)
    {
        float cur = 0.0f;

        while (cur < duration)
        {
            cur += Time.deltaTime;

            this.transform.Translate(direction * speed * Time.deltaTime);

            yield return null;
        }

        UnUseProjectile();
    }
}
