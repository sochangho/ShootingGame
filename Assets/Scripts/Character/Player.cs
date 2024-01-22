using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private MoveVirtualJoystick virtualJoystickMove;

    private AttackVirtualJoystick attackVirtualJoystick;


    public bool IsVirtualMoveControl {get;set;}

    override public void Created() { 
        base.Created();
        virtualJoystickMove = FindObjectOfType<MoveVirtualJoystick>();
        attackVirtualJoystick = FindObjectOfType<AttackVirtualJoystick>();
    }

    override public void Destroyed() { base.Destroyed(); }

    override public void Active() { base.Active(); }

    override public void InActive() { base.InActive(); }

    public override void CharacterUpdate()
    {

        MovePlayer();
    
    }

    public void MovePlayer()
    {

        if (!IsVirtualMoveControl)
        {
            InputKeyMove();
        }
        
        VirtualJoystickMove();
        VirtualJoystickRotation();
    }

    public override void DirectionUpdate(Vector3 direction)
    {
 
    }

    
    public override void Die()
    {
        
    }


    #region PlayerController

    public void InputKeyMove()
    {
        float h = Input.GetAxis("Horizontal");

        float v = Input.GetAxis("Vertical");

         
        Vector3 dir = new Vector3(h, 0, v) * -1;
        dir = dir.normalized;

        if (dir != Vector3.zero)
        {

            transform.position += dir * 10 * Time.deltaTime;

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

        Vector3 dir = new Vector3(virtualJoystickMove.GetX, 0, virtualJoystickMove.GetY) * -1;
        transform.position += dir * 10 * Time.deltaTime;

       
    }

    public void VirtualJoystickRotation()
    {
        Vector3 dir;

        if (attackVirtualJoystick.IsAiming)
        {
            dir = new Vector3(attackVirtualJoystick.GetX, 0, attackVirtualJoystick.GetY).normalized;
            PlayerRotation(dir);
        }
        else
        {
            if (IsVirtualMoveControl)
            {
                dir = new Vector3(virtualJoystickMove.GetX, 0, virtualJoystickMove.GetY).normalized * -1;
                PlayerRotation(dir);
            }
        }
    }


    public void PlayerRotation(Vector3 dir)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10);
    }


    

    #endregion
}
