using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

[RequireComponent(typeof (BoxCollider))]
public class Wheat : MonoBehaviour
{
    [SerializeField] Transform panelCut;
    [SerializeField] GameObject wheatView;

    private GameObject bottomPart;
    private GardenBed gardenBed;
    private BlockPool blockPool;

    public enum State
    {
        harvestedCrop,
        notHarvested—rop
    }

    public State state = State.notHarvested—rop;

    private void Start()
    {
        gardenBed = transform.parent.GetComponent<GardenBed>();
        blockPool = BlockPool.Instance;

        Material materialSlicedObject = wheatView.GetComponent<MeshRenderer>().material;

        SlicedHull slicedObjects = CutObject(wheatView, materialSlicedObject);
        //GameObject topPart = slicedObjects.CreateUpperHull(wheatView, materialSlicedObject);
        bottomPart = slicedObjects.CreateLowerHull(wheatView, materialSlicedObject);

        bottomPart.transform.position = transform.position;
        //topPart.transform.position = transform.position;
        bottomPart.transform.parent = transform;

    }

    public void Harvesting()
    {
        if (state == State.notHarvested—rop)
        {
            HarvestedState();

            Transform block;

            if (blockPool.HasPoolObject(out PoolObject poolObj))
            { 
                block = poolObj.transform;
            }
            else
            {
                block = blockPool.GetPoolObject().transform;
            }
            block.position = transform.position + Vector3.up * 0.1f;
            block.rotation = transform.rotation;
            block.gameObject.SetActive(true);

            wheatView.SetActive(false);
            bottomPart.SetActive(true);
        }
    }

    public void NotHarvestedState()
    {
        wheatView.SetActive(true);
        bottomPart.SetActive(false);

        state = State.notHarvested—rop;
        GetComponent<BoxCollider>().enabled = true;
    }

    private void HarvestedState()
    {
        if(gardenBed!=null)
            gardenBed.WheatWasCut();
        state = State.harvestedCrop;
        GetComponent<BoxCollider>().enabled = false;
    }

    private SlicedHull CutObject(GameObject slicedObject, Material crossSectionMaterial)
    {
        return slicedObject.Slice(panelCut.position, panelCut.up, crossSectionMaterial);
    }
}
