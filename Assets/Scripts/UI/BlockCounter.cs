using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockCounter : MonoBehaviour
{
    [SerializeField] TMP_Text counterBlockTxt;
    [SerializeField] GameObject fullTxt;
    [SerializeField] float timeShowWarning;

    private PlayerStat playerStat;
    private Coroutine disableText;

    private void Start()
    {
        playerStat = PlayerStat.Instance; 
        ChangeQuantityBlock();
        EventManager.Instance.numberBlocksChanged += ChangeQuantityBlock;
    }

    private void OnDisable()
    {
        EventManager.Instance.numberBlocksChanged -= ChangeQuantityBlock;
    }

    private void ChangeQuantityBlock()
    {
        counterBlockTxt.text = playerStat.CurrentBlockInStack + "/" + playerStat.maxBlockInStack;

        if (playerStat.CurrentBlockInStack == playerStat.maxBlockInStack)
            StackOverFlow();
    }

    private void StackOverFlow()
    {
        fullTxt.SetActive(true);

        if (disableText != null)
            StopCoroutine(disableText);

        disableText = StartCoroutine(DisableText());
    }

    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(timeShowWarning);
        fullTxt.SetActive(false);
    }
}
