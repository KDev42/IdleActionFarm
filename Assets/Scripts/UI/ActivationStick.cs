using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivationStick : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] GameObject startingTxt;
    [SerializeField] GameObject stick;

    private void Start()
    {
        EventManager.Instance.stopMoveStick += DisableStick;
    }

    private void OnDisable()
    {
        EventManager.Instance.stopMoveStick -= DisableStick;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        stick.transform.position = eventData.position;
        startingTxt.SetActive(false);
        stick.SetActive(true);
    }

    private void DisableStick()
    {
        stick.SetActive(false);
    }
}
