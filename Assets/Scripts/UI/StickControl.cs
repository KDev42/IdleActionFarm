
using UnityEngine;

public class StickControl : MonoBehaviour
{
    [SerializeField] float minDistTM;
    [SerializeField] float maxDistTM;

    private int closestTouch;
    private float distanceToStick;
    private Vector2 inputMouse;
    private Vector2 thisPosition;
    private Transform touchMarker;
    private EventManager eventManager;

    private void Start()
    {
        eventManager = EventManager.Instance;
        thisPosition = transform.position;
        touchMarker = transform.GetChild(0);

    }

    private void Update()
    {
        //#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            inputMouse = Input.mousePosition;
            if ((thisPosition - inputMouse).magnitude > minDistTM && (thisPosition - inputMouse).magnitude < maxDistTM)
            {
                touchMarker.position = inputMouse;
                eventManager.MoveStick(inputMouse - thisPosition);
            }
            else
            {
                touchMarker.position = thisPosition;
                eventManager.StopMoveStick();
            }
        }
        else
        {
            touchMarker.position = thisPosition;
            eventManager.StopMoveStick();
        }
        //#endif
        //#if ANDROID
        //if (Input.touchCount > 0)
        //{
        //    distanceToStick = 100000;
        //    for (int i = 0; i < Input.touchCount; i++)
        //    {
        //        if ((Input.GetTouch(i).position - thisPosition).magnitude < distanceToStick)
        //        {
        //            distanceToStick = (Input.GetTouch(i).position - thisPosition).magnitude;
        //            closestTouch = i;
        //        }
        //    }

        //    inputMouse = Input.GetTouch(closestTouch).position;
        //    if ((thisPosition - inputMouse).magnitude > minDistTM && (thisPosition - inputMouse).magnitude < maxDistTM)
        //    {
        //        touchMarker.transform.position = inputMouse;
        //        eventManager.MoveStick(inputMouse - thisPosition);
        //    }
        //    else
        //    {
        //        eventManager.StopMoveStick();
        //        touchMarker.transform.position = thisPosition;
        //    }
        //}
        //else
        //{
        //    eventManager.StopMoveStick();
        //    touchMarker.transform.position = thisPosition;
        //}
        //#endif
    }
}
