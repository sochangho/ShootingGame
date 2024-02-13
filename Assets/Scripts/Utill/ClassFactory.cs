using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;

public class ClassFactory 
{
    static Assembly assembly;

    public static AICharacterAttack GetClassInstanceAIAttack(string className)
    {


        if (assembly == null)
        {
            assembly = Assembly.GetExecutingAssembly();
        }

        System.Type t = assembly.GetType(className);


        object obj = System.Activator.CreateInstance(t);

        if(obj == null)
        {
            Debug.LogError($" No Exit className {className}");
            return null;
        }

        var at =  (obj as AICharacterAttack);

        return at;
    }

    public static AbilityCalculate GetClassInstanceAbilityCalculate(string className)
    {
        if (assembly == null)
        {
            assembly = Assembly.GetExecutingAssembly();
        }

        System.Type t = assembly.GetType(className);


        object obj = System.Activator.CreateInstance(t);

        if (obj == null)
        {
            Debug.LogError($" No Exit className {className}");
            return null;
        }

        var c = (obj as AbilityCalculate);

        return c;
    }


    public static WeaponAction GetClassInstanceWeaponActon(string className)
    {
        if (assembly == null)
        {
            assembly = Assembly.GetExecutingAssembly();
        }

        System.Type t = assembly.GetType(className);


        object obj = System.Activator.CreateInstance(t);

        if (obj == null)
        {
            Debug.LogError($" No Exit className {className}");
            return null;
        }

        var c = (obj as WeaponAction);

        return c;


    }


}
