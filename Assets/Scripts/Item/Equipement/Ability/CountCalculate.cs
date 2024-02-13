using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCalculate : AbilityCalculate
{
    public override float IncreaseAbility(float value, float ability)
    {
        ability +=  value;

        return ability;
    }

    public override float DecreaseAbility(float value, float ability)
    {
        ability -=  value ;

        return ability;
    }
}
