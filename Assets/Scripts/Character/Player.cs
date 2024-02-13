using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character 
{
    [SerializeField]
    private Transform shotStartPoint;

    [SerializeField]
    private ShotProgress shotProgress;

    private MoveVirtualJoystick moveVirtualJoystickMove;

    private AttackVirtualJoystick attackVirtualJoystick;

    private BoxCollider collider;

    public PlayerInfo playerInfo { get; protected set; }

    private bool isChaging = false;

    private bool isAttacking = false;

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
            playerInfo = (PlayerInfo)characterInfo;
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

        shotProgress.Charge(playerInfo.Shot_Count);

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
        UpdateItem();

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


    public override void Attack()
    {
        if (!isMove)
        {
            return;
        }

        if (isDie)
        {
            return;
        }


        if (isChaging)
        {
            Debug.Log("공격할 수 없다.");
            return;
        }


        if(playerInfo == null)
        {
            playerInfo = (PlayerInfo)characterInfo;
        }

        

        shotProgress.Use();

        base.Attack();

        if (!playerInfo.ShotDecrease())
        {
            isChaging = true;
            shotProgress.Recharge(playerInfo, () =>
            {
                playerInfo.ShotCharge();
                isChaging = false;
            });
        }

    }


    public override void Die()
    {
        isDie = true;
        CharacterAnimator.SetTrigger("Die");
    }

    public override void DieAnimationEvent()
    {
        NotifyObserver();
        this.gameObject.SetActive(false);
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

        if (CheackHitObjects(dir))
            dir = Vector3.zero;

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

        if (CheackHitObjects(dir))
            dir = Vector3.zero;

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

    private bool CheackHitObjects(Vector3 movement)
    {

        if(collider == null)
        {
           collider =  GetComponent<BoxCollider>();
        }

        //movement = transform.TransformDirection(movement);
        // scope로 ray 충돌을 확인할 범위를 지정할 수 있다.
        float scope = 1f;

        // 플레이어의 머리, 가슴, 발 총 3군데에서 ray를 쏜다.
        List<Vector3> rayPositions = new List<Vector3>();
        rayPositions.Add(transform.position + Vector3.up * 0.1f);
        rayPositions.Add(transform.position + Vector3.up * collider.size.y * 0.5f);
        rayPositions.Add(transform.position + Vector3.up * collider.size.y);

        // 디버깅을 위해 ray를 화면에 그린다.
        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * scope, Color.red);
        }

        // ray와 벽의 충돌을 확인한다.
        foreach (Vector3 pos in rayPositions)
        {
            if (Physics.Raycast(pos, movement, out RaycastHit hit, scope))
            {
                if (hit.collider.CompareTag("Objects"))
                    return true;
            }
        }
        return false;

    }



}


public partial class Player : Character
{

   private List<Equipment> equipments = new List<Equipment>();


   

   public void UseItem(Equipment equipment)
   {
        equipments.Add(equipment);
        equipment.UseItem();

        WeaponReposiotion("아이템 추가");


   }
   
   public void UnUseItem(int id)
   {
        var e = equipments.Find(x => x.Item_Id == id);

        if (e != null)
        {          
            equipments.Remove(e);
            WeaponReposiotion("아이템 삭제");
        }

        //WeaponReposiotion("아이템 삭제");
   }
   
   public void WaveEnd()
   {
      for(int i = equipments.Count - 1; i >= 0; --i)
      {
            equipments[i].EndWaveItemUpdateEvent();
            equipments[i].WaveEnd();
            
      }
    }

   public void UpdateItem()
   {

        foreach(var e in equipments)
        {
            e.UpdateItem();
        }

   }

    public void WeaponReposiotion(string txt)
    {
        List<Weapon> wList = new List<Weapon>(); 

        foreach(var e in equipments)
        {
            if(e is Weapon)
            {
                var w = e as Weapon;

                wList.Add(w);
            }
        }


        Debug.Log($"<color=green> 무기  {txt}  , 총 무기 개수 {wList.Count} </color>");

        if(wList.Count == 0)
        {
            return;
        }


        float angle = 360 / wList.Count;

        float sumAngle = 0;

        foreach(var w in wList)
        {
            Debug.Log($" 회전 각도  {sumAngle}");
            w.WeaponReposiotion(sumAngle);
            sumAngle += angle;
        }


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
