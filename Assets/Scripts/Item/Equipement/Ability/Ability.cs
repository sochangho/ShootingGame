using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : Equipment
{
    protected Table_Item.ItemAbilityInfo itemAbilityInfo;

    protected AbilityCalculate abilityCalculate;

    public Ability(Table_Item.ItemAbilityInfo itemAbilityInfo)
    {
        this.itemAbilityInfo = itemAbilityInfo;

        Item_Id = itemAbilityInfo.Id;
        Item_Name = itemAbilityInfo.Name;
        Count = itemAbilityInfo.Duration;

        abilityCalculate = ClassFactory.GetClassInstanceAbilityCalculate(itemAbilityInfo.CalculateClass);

    }

    public override void AddItem()
    {
        var player = GameScene.Instance.GameRoopController.player;
        PlayerInfo playerInfo = player.playerInfo;

        PlayerAbilityValue playerAbilityValue = new PlayerAbilityValue();

        playerAbilityValue.Attack = abilityCalculate.IncreaseAbility(itemAbilityInfo.Attack, playerInfo.Attack);
        playerAbilityValue.Defence = abilityCalculate.IncreaseAbility(itemAbilityInfo.Defence, playerInfo.Defence);
        playerAbilityValue.Speed = abilityCalculate.IncreaseAbility(itemAbilityInfo.Speed, playerInfo.Speed);
        playerAbilityValue.Shot_Duration = abilityCalculate.IncreaseAbility(itemAbilityInfo.Shot_Duration, playerInfo.Shot_Duration);
        playerAbilityValue.Shot_LoadSpeed = abilityCalculate.IncreaseAbility(itemAbilityInfo.Shot_LoadSpeed, playerInfo.Shot_LoadSpeed);
        playerAbilityValue.Shot_Speed = abilityCalculate.IncreaseAbility(itemAbilityInfo.Shot_Spped, playerInfo.Shot_Speed);

        

        playerInfo.PlayerAbilitySet(playerAbilityValue);

        base.AddItem();

    }

    public override void UpdateItem()
    {
        var player = GameScene.Instance.GameRoopController.player;
        PlayerInfo playerInfo = player.playerInfo;

        
    }

    public override void RemoveItem()
    {
        var player = GameScene.Instance.GameRoopController.player;
        PlayerInfo playerInfo = player.playerInfo;

        PlayerAbilityValue playerAbilityValue = new PlayerAbilityValue();

        playerAbilityValue.Attack = abilityCalculate.DecreaseAbility(itemAbilityInfo.Attack, playerInfo.Attack);
        playerAbilityValue.Defence = abilityCalculate.DecreaseAbility(itemAbilityInfo.Defence, playerInfo.Defence);
        playerAbilityValue.Speed = abilityCalculate.DecreaseAbility(itemAbilityInfo.Speed, playerInfo.Speed);
        playerAbilityValue.Shot_Duration = abilityCalculate.DecreaseAbility(itemAbilityInfo.Shot_Duration, playerInfo.Shot_Duration);
        playerAbilityValue.Shot_LoadSpeed = abilityCalculate.DecreaseAbility(itemAbilityInfo.Shot_LoadSpeed, playerInfo.Shot_LoadSpeed);
        playerAbilityValue.Shot_Speed = abilityCalculate.DecreaseAbility(itemAbilityInfo.Shot_Spped, playerInfo.Shot_Speed);
        

        playerInfo.PlayerAbilitySet(playerAbilityValue);

        base.RemoveItem();
    }

    public override void WaveEnd()
    {
        var player = GameScene.Instance.GameRoopController.player;
        PlayerInfo playerInfo = player.playerInfo;

        base.WaveEnd();
    }

    public override void EndWaveItemUpdateEvent()
    {
        
    }


}
