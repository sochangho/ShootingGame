using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : CameraObject 
{

    private Player player;

    private bool isGetPlayer = false ;

    [Range(-20, 20)]
    public float diffX;

    [Range(-20, 20)]
    public float diffY;

    [Range(-20, 20)]
    public float diffZ;

    [Range(0, 1)]
    public float followSpeed;

   
    public override void Created()
    {
        base.Created();
       
    }


    public void SetFollowPlayer(Player player)
    {
        this.player = player;
        Debug.Log("SetFollowPlayer");
        isGetPlayer = true;
    }


    public override void CameraUpdate()
    {

        if (!isGetPlayer)
        {
           
            return;
        }

      

        var playerPos = player.gameObject.transform.position;

        this.transform.position = Vector3.Lerp(this.transform.position, playerPos + new Vector3(diffX , diffY, diffZ) , followSpeed);

        this.transform.LookAt(player.transform);


    }


}
