using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Character : BaseObject
{
    public string CharacterName { get; private set; }

    public CharaterInfo CharaterInfo { get;  protected set; }


    public Animator CharacterAnimator;


    #region Override & Virtual
    override public void Created()
    {
        CharacterName = this.gameObject.name.Replace("(Clone)","");                
        Debug.Log($"{CharacterName}  ����");

    }

    override public void Destroyed()
    {
        Debug.Log($"{CharacterName}  �ı�");

    }
    
    override public void Active(){

        Debug.Log($"{CharacterName}  Ȱ��");

    }

    override public void InActive(){

        Debug.Log($"{CharacterName}  ��Ȱ��");

    }

    virtual protected void AniWalk(bool isWalk)
    {
        CharacterAnimator.SetBool("IsWalk",isWalk);
    }

    virtual protected void AniAttack()
    {
        CharacterAnimator.SetTrigger("Attack");
    }

    virtual public void Attack()
    {
        AniAttack();
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

    abstract public void CharacterUpdate();

    abstract public void DirectionUpdate(Vector3 direction);

    abstract public void Die();
  
    #endregion






}


