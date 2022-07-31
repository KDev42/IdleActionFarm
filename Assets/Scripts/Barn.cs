using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : MonoBehaviour
{
    [SerializeField] Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            EventManager.Instance.EnterBarm(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            EventManager.Instance.ExitBarn();
        }
    }
}
