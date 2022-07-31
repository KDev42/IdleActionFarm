using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Wheat>())
        {
            other.GetComponent<Wheat>().Harvesting();
        }
    }
}
