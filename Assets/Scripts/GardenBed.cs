using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    [SerializeField] float growthTime;

    private List<Wheat> wheatPool;

    private int quntityWheat;
    private int quantityCutWheat;
    private Coroutine newHarvest;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Character>())
        {
            other.gameObject.GetComponent<Character>().InsideGardenBed();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            other.gameObject.GetComponent<Character>().OutsideGardenBad();
        }
    }

    private void Start()
    {
        wheatPool = new List<Wheat>();

        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Wheat>())
            {
                quntityWheat++;
                wheatPool.Add(transform.GetChild(i).GetComponent<Wheat>());
            }
        }
    }

    public void WheatWasCut()
    {
        quantityCutWheat++;

        if (quantityCutWheat == quntityWheat)
        {
            newHarvest = StartCoroutine(NewHarvest());
        }
    }

    IEnumerator NewHarvest()
    {
        yield return new WaitForSeconds(growthTime);
        foreach(Wheat wheat in wheatPool)
        {
            wheat.NotHarvestedState();
        }
        quantityCutWheat = 0;
    }
}
