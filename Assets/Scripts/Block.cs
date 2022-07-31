using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent (typeof(PoolObject))]
public class Block : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] float speed;
    [SerializeField] float lifetime;

    private Coroutine desactivation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            if (PlayerStat.Instance.CurrentBlockInStack < PlayerStat.Instance.maxBlockInStack)
            {
                PickUp(other.GetComponent<Character>().stack);
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void OnEnable()
    {
        transform.GetComponent<BoxCollider>().enabled = true;
        desactivation = StartCoroutine(Desactivation());
    }

    private void OnDisable()
    {
        if(desactivation!=null)
            StopCoroutine(desactivation);
    }

    public void PickUp(Transform stack)
    {
        StopCoroutine(desactivation);
        transform.parent = stack;
        Vector3 targetPosition = stack.GetComponent<Stack>().GetBlockPositionInStack(transform);
        ParabolaMove(transform.localPosition, targetPosition, stack);
        transform.localRotation = stack.localRotation;
    }

    public void Put(Transform targetObj)
    {
        transform.parent = null;
        ParabolaMove(transform.localPosition, targetObj.position, targetObj, GotIntoBarn);
    }

    private void ParabolaMove(Vector3 startPosition, Vector3 targetPosition, Transform targetObj, Action action = null)
    {
        Tween tween =  DOTween.To(setter: value =>
        {
            transform.localPosition = MyMath.Parabola(startPosition, targetPosition, height, value);
            transform.rotation = targetObj.rotation;
        }, startValue: 0, endValue: 1, duration: speed)
            .SetEase(Ease.Linear);

        if(action!=null)
            tween.OnComplete(()=> action());
    }

    private void GotIntoBarn()
    {
        EventManager.Instance.GotIntoBarn();
        GetComponent<PoolObject>().ReturnToPool();
    }

    IEnumerator Desactivation()
    {
        yield return new WaitForSeconds(lifetime);
        transform.GetComponent<BoxCollider>().enabled = false;
        gameObject.SetActive(false);
    }
}
