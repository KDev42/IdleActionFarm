using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance { get; private set; }

    public int maxBlockInStack;
    public int CurrentBlockInStack { get; set; }
    public int Coins { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(GetComponent<PlayerStat>());
        }
    }

    public void AddBlockInStack(int quntityBlock=1)
    {
        CurrentBlockInStack += quntityBlock;
        EventManager.Instance.NumberBlocksChanged();
    }

    public void SubtractBlockInStack(int quntityBlock = 1)
    {
        CurrentBlockInStack -= quntityBlock;
        EventManager.Instance.NumberBlocksChanged();
    }
}
