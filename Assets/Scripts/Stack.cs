using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stack : MonoBehaviour
{
    [SerializeField] int quantityBlocksX;
    [SerializeField] int quantityBlocksZ;
    [SerializeField] Vector3 sizeBlock;
    [SerializeField] float deltaUnload;

    private Vector3 zeroPosition;
    private List<Transform> blocks;
    private Coroutine delayUnload;
    private PlayerStat playerStat;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerStat = PlayerStat.Instance;

        zeroPosition =  new Vector3(-(quantityBlocksX / 2.0f - 0.5f) * sizeBlock.x, 0, 0);
        blocks = new List<Transform>(playerStat.maxBlockInStack);

        EventManager.Instance.enterBarn += Unload;
        EventManager.Instance.exitBarn += StopUnload;
        EventManager.Instance.moveStick += StartAnimation;
        EventManager.Instance.stopMoveStick += StopAnimation;
    }

    private void OnDisable()
    {
        EventManager.Instance.enterBarn -= Unload;
        EventManager.Instance.exitBarn -= StopUnload;
        EventManager.Instance.moveStick -= StartAnimation;
        EventManager.Instance.stopMoveStick -= StopAnimation;
    }

    public Vector3 GetBlockPositionInStack(Transform block)
    {
        int blocksInLine = quantityBlocksZ * quantityBlocksX;
        int indexInLine = playerStat.CurrentBlockInStack % blocksInLine;
        int x = indexInLine % quantityBlocksX;
        int z = indexInLine / quantityBlocksZ;
        int y = playerStat.CurrentBlockInStack / blocksInLine;

        playerStat.AddBlockInStack();
        blocks.Add(block);

        return zeroPosition + new Vector3(x*sizeBlock.x, y*sizeBlock.y, -z*sizeBlock.z);
    }

    private void Unload(Transform target)
    {
        delayUnload = StartCoroutine(DelayUnload(target));
    }

    private void StopUnload()
    {
        if(delayUnload!=null)
            StopCoroutine(delayUnload);
    }

    IEnumerator DelayUnload(Transform target)
    {
        for (int i = playerStat.CurrentBlockInStack - 1; i >= 0; i--)
        {
            blocks[i].GetComponent<Block>().Put(target);
            blocks.RemoveAt(i);
            playerStat.SubtractBlockInStack();
            yield return new WaitForSeconds(deltaUnload);
        }
    }

    private void StartAnimation(Vector3 directionMove)
    {
        animator.SetBool("Swing", true);
    }

    private void StopAnimation()
    {
        animator.SetBool("Swing", false);
    }
}
