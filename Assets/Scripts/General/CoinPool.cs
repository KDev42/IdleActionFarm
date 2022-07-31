using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    [SerializeField] PoolObject prefab;
    [SerializeField] Transform container;
    [SerializeField] bool autoExpand;
    [SerializeField] int startCount;

    public static CoinPool Instance { get; private set; }

    public Pool pool { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(GetComponent<CoinPool>());
        }
    }

    private void Start()
    {
        pool = new Pool(prefab, startCount, container);
        pool.autoExpand = autoExpand;
    }

    public PoolObject GetPoolObject()
    {
        return pool.GetFreeElement();
    }

    public bool HasPoolObject(out PoolObject poolObject)
    {
        return pool.HasFreeElemant(out poolObject);
    }
}
