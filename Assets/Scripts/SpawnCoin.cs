using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnCoin : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform target;
    [SerializeField] Transform parentCoins;
    [SerializeField] float duration;
    [SerializeField] Vector3 endCoinScale;
    [SerializeField] Vector3 startScale;

    private CoinPool coinPool;

    private void Start()
    {
        EventManager.Instance.gotIntoBarn += Spawn;
        coinPool = CoinPool.Instance;
    }

    private void OnDisable()
    {
        EventManager.Instance.gotIntoBarn -= Spawn;
    }

    private void Spawn()
    {
        Transform coin;
        Vector3 spawnScreenPotion = Camera.main.WorldToScreenPoint(transform.position);

        if (coinPool.HasPoolObject(out PoolObject poolObj))
        {
            coin = poolObj.transform;
        }
        else
        {
            coin = coinPool.GetPoolObject().transform;
        }
        coin.localScale = startScale;
        coin.position = spawnScreenPotion;
        coin.rotation = transform.rotation;
        coin.SetParent(parentCoins);
        //coin = Instantiate(coinPrefab, spawnScreenPotion, transform.rotation, parentCoins);

        Tween tween = coin.transform.DOMove(target.position, duration);
        coin.DOScale(endCoinScale, duration);

        tween.OnComplete(() => ReachedTarget(coin));
    }

    private void ReachedTarget(Transform obj)
    {
        EventManager.Instance.AddCoins();
        obj.GetComponent<PoolObject>().ReturnToPool();
    }
}
