using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateUI : MonoBehaviour
{
    [SerializeField]
    private HpProgress hpProgress;
   
    public void CharacterAttackedObserverResister(Character character)
    {        
        character.baseAttacked.ResisterObserver(hpProgress);

    }

    public void CharacterRefeshObserverResister(Character character)
    {
        character.characterInfo.ResisterObserver(hpProgress);

    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + GameScene.Instance.GameRoopController.GetCameraTransform().forward);
    }

  

}
