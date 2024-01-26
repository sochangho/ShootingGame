using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;


public class AttackVirtualJoystick : VirtualJoystick 
{
    public bool IsAiming { get; private set; } = false;

    private WaitForSeconds attackDelaySeconds = new WaitForSeconds(0.5f); 

    override public void PointerDown(PointerEventData eventData)
    {

        IsAiming = true;

    }
    override public void PointerDrag(PointerEventData eventData)
    {
        // 조준
        // 

        touchPosition = Vector2.zero;



        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystick.rectTransform, eventData.position,
            eventData.pressEventCamera, out touchPosition
            ))
        {

            //0~1
            touchPosition.x = (touchPosition.x / joystick.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / joystick.rectTransform.sizeDelta.y);


            // -n ~ n
            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);


            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

           

            joystickContorller.rectTransform.anchoredPosition
                = new Vector2(touchPosition.x * joystick.rectTransform.sizeDelta.x / 2
                , touchPosition.y * joystick.rectTransform.sizeDelta.y / 2
                );

        }


    }
    override public void PointerUp(PointerEventData eventData)
    {
        // 공격
        GameScene.Instance.GameRoopController.player.Attack();

        StartCoroutine(AttackDelayRoutin());
        joystickContorller.rectTransform.anchoredPosition = Vector2.zero;

    }


    IEnumerator AttackDelayRoutin()
    {
        yield return attackDelaySeconds;

        IsAiming = false;
    } 


}
