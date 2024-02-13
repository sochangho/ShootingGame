using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentCalculate : AbilityCalculate
{
    public override float IncreaseAbility(float value, float ability)
    {
        ability += ability * value / 100;

        return ability;
    }

    public override float DecreaseAbility(float value, float ability)
    {
        ability -= ability * value / 100;
       
        return ability;
    }
}
