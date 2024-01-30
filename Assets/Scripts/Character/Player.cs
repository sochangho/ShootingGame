using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character 
{
    [SerializeField]
    private Transform shotStartPoint;

    private MoveVirtualJoystick moveVirtualJoystickMove;

    private AttackVirtualJoystick attackVirtualJoystick;

    
    public bool IsVirtualMoveControl {get;set;}


    #region Base

    override public void Created() { 
        base.Created();
  
    }

    override public void Destroyed() { base.Destroyed(); }

    override public void Active() { base.Active(); }

    override public void InActive() { base.InActive(); }

    #endregion


    #region Override

    public override void CharacterCreated(int id)
    {
        var d = InfoManager.Instance.TablePlayer.GetInfoById(id);

        if (characterInfo == null)
        {
            characterInfo = new PlayerInfo();
            characterInfo.SetInfo(d);
        }
        if (baseAttack == null)
        {
            baseAttack = new ShotAttack(characterInfo);
        }
        if (baseAttacked == null)
        {
            baseAttacked = new BaseAttacked(characterInfo);
            baseAttacked.ResisterObserver(this);
        }

        ObserversResister();
    }

    public override void CharacterUpdate()
    {
        MovePlayer();    
    }

    public override Vector3 GetAimDirection()
    {
        return new Vector3(attackVirtualJoystick.GetX, 0, attackVirtualJoystick.GetY).normalized ;
    }


    public override Vector3 GetShotStartPos()
    {
        return shotStartPoint.position;
    }

    public void MovePlayer()
    {
        if (!isMove)
        {
            return;
        }

        if (isDie)
        {
            return;
        }

        if (!IsVirtualMoveControl)
        {
            InputKeyMove();
        }
        
        VirtualJoystickMove();
        VirtualJoystickRotation();
    }


    
    public override void Die()
    {
        isDie = true;
        CharacterAnimator.SetTrigger("Die");
    }

    public override void DieAnimationEvent()
    {
        NotifyObserver();
    }

    public override void UpdateData(object data)
    {
        base.UpdateData(data);
    }

    #endregion


 

}



public partial class Player : Character
{

    private bool isMove = false;

    public void IsMovePlayer()
    {
        isMove = true;
    }

    public void DontMovePlayer()
    {
        isMove = false;
    }


    public void SetJoistick(MoveVirtualJoystick moveVirtualJoystick, AttackVirtualJoystick attackVirtualJoystick)
    {
        this.moveVirtualJoystickMove = moveVirtualJoystick;
        this.attackVirtualJoystick = attackVirtualJoystick;
    }


    public void InputKeyMove()
    {
        float h = Input.GetAxis("Horizontal");

        float v = Input.GetAxis("Vertical");


        Vector3 dir = new Vector3(h, 0, v) * -1;
        dir = dir.normalized;

        if (dir != Vector3.zero)
        {

            transform.position += dir * characterInfo.Speed * Time.deltaTime;

            InputKeyRotation(dir);

        }

        Walk(dir != Vector3.zero);
    }

    public void InputKeyRotation(Vector3 dir)
    {
        if (attackVirtualJoystick.IsAiming)
        {
            dir = new Vector3(attackVirtualJoystick.GetX, 0, attackVirtualJoystick.GetY).normalized;
        }


        PlayerRotation(dir);
    }

    public void VirtualJoystickMove()
    {
        if (!IsVirtualMoveControl)
        {
            return;
        }

        Vector3 dir = new Vector3(moveVirtualJoystickMove.GetX, 0, moveVirtualJoystickMove.GetY) * -1;
        transform.position += dir * characterInfo.Speed * Time.deltaTime;

    }

    public void VirtualJoystickRotation()
    {
        Vector3 dir;

        if (attackVirtualJoystick.IsAiming)
        {
            dir = GetAimDirection();
            PlayerRotation(dir);
        }
        else
        {
            if (IsVirtualMoveControl)
            {
                dir = new Vector3(moveVirtualJoystickMove.GetX, 0, moveVirtualJoystickMove.GetY).normalized * -1;
                PlayerRotation(dir);
            }
        }
    }


    public void PlayerRotation(Vector3 dir)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10);
    }





}



public partial class Player : Character, ISubject
{

    List<IObserver> observers = new List<IObserver>();

    #region Interface
    public void ResisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObserver()
    {
        for(int i = 0; i < observers.Count; ++i)
        {
            observers[i].UpdateData(typeof(Player).Name);
        }
    }

    #endregion

}
