using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtillGizmo 
{
    
    static public void RenderSphere(Vector3 vector3 , float radius)
    {
#if UNITY_EDITOR
        Gizmos.DrawWireSphere(vector3, radius);
#endif
    }

}
