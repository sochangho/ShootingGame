using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// ���� �� ���� idle �� �����̰� �ִ°�??
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
