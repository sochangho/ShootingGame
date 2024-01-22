using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    protected Image joystick;
    protected Image joystickContorller;

    protected Vector2 touchPosition;

    public float GetX => touchPosition.x;
    public float GetY => touchPosition.y;



    void Awake()
    {
        joystick = GetComponent<Image>();
        joystickContorller = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        PointerDown(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {

        PointerDrag(eventData);

    
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp(eventData);
    }



    abstract public void PointerDown(PointerEventData eventData);
    abstract public void PointerDrag(PointerEventData eventData);
    abstract public void PointerUp(PointerEventData eventData); 
    

}
