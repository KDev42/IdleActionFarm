using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] TMP_Text counterCoin;
    [SerializeField] Animator coinAnimator;

    private Coroutine stopAnimation;

    private void Start()
    {
        EventManager.Instance.addCoins += AddCoins;
    }

    private void OnDisable()
    {
        EventManager.Instance.addCoins -= AddCoins;
    }

    private void AddCoins()
    {
        PlayerStat playerStat = PlayerStat.Instance;

        playerStat.Coins += 15;
        counterCoin.text = playerStat.Coins.ToString();

        coinAnimator.SetBool("vibration", true);

        if (stopAnimation != null)
            StopCoroutine(stopAnimation);

        stopAnimation = StartCoroutine(StopAnimation());
    }

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(1);
        coinAnimator.SetBool("vibration", false);
    }
}
