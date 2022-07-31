using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    [SerializeField] PoolObject prefab;
    [SerializeField] Transform container;
    [SerializeField] bool autoExpand;
    [SerializeField] int startCount;

    public static BlockPool Instance { get; private set; }

    public Pool pool { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(GetComponent<BlockPool>());
        }
    }

    private void Start()
    {
        pool = new  Pool(prefab, startCount, container);
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
