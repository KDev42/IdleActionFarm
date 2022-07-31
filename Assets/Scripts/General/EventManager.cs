using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public delegate void acceptVector3Action(Vector3 vector);
    public delegate void acceptTransformAction(Transform vector);
    public delegate void simpleAction();
    public event acceptVector3Action moveStick;
    public event acceptTransformAction enterBarn;
    public event simpleAction exitBarn;
    public event simpleAction stopMoveStick;
    public event simpleAction gotIntoBarn;
    public event simpleAction addCoins;
    public event simpleAction numberBlocksChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(GetComponent<EventManager>());
        }
    }

    public void AddCoins()
    {
        if (addCoins != null)
            addCoins();
    }

    public void MoveStick(Vector3 direction)
    {
        if (moveStick != null)
            moveStick(direction);
    }

    public void StopMoveStick()
    {
        if (stopMoveStick != null)
            stopMoveStick();
    }

    public void EnterBarm(Transform tr)
    {
        if (enterBarn != null)
            enterBarn(tr);
    }

    public void ExitBarn()
    {
        if (exitBarn != null)
            exitBarn();
    }

    public void GotIntoBarn()
    {
        if (gotIntoBarn != null)
            gotIntoBarn();
    }

    public void NumberBlocksChanged()
    {
        if (numberBlocksChanged != null)
            numberBlocksChanged();
    }
}
