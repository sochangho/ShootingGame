using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 공격 및 추적 idle 후 딜레이가 있는가??
public class AIDelay
{
    public float delay;

    public WaitForSeconds WaitForSeconds;

    public AIDelay(float delay)
    {
        this.delay = delay;

        WaitForSeconds = new WaitForSeconds(delay);
    }

 
}
