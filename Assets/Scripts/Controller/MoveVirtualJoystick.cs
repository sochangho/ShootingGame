using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveVirtualJoystick : VirtualJoystick
{

    override public void PointerDown(PointerEventData eventData)
    {

        GameScene.Instance.GameRoopController.player.IsVirtualMoveControl = true;
        GameScene.Instance.GameRoopController.player.Walk(true);

    }

    override public void PointerDrag(PointerEventData eventData)
    {

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

        GameScene.Instance.GameRoopController.player.IsVirtualMoveControl = false;
        GameScene.Instance.GameRoopController.player.Walk(false);
        joystickContorller.rectTransform.anchoredPosition = Vector2.zero;

    }




}
