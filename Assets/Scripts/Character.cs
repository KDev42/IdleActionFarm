using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject scythe;
    [SerializeField] float speed;
    public Transform stack;

    private EventManager eventManager;
    private Animator anim;
    private Rigidbody thisRigidbody;

    private void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();

        eventManager = EventManager.Instance;
        anim = GetComponent<Animator>();
        eventManager.moveStick += Move;
        eventManager.stopMoveStick += StopMove;
    }

    private void OnDisable()
    {
        eventManager.moveStick -= Move;
        eventManager.stopMoveStick -= StopMove;
    }

    public void InsideGardenBed()
    {
        scythe.SetActive(true);
        anim.SetBool("Harvesting", true);
    }

    public void OutsideGardenBad()
    {
        scythe.SetActive(false);
        anim.SetBool("Harvesting", false);
    }

    private void Move(Vector3 directio)
    {
        transform.LookAt(transform.position + new Vector3(directio.x, 0, directio.y));
        //transform.position += transform.forward * speed * Time.deltaTime;
        thisRigidbody.AddForce(transform.forward * speed * Time.deltaTime,ForceMode.Force);

        anim.SetBool("CanRun", true);
    }

    private void StopMove()
    {
        anim.SetBool("CanRun", false);
    }
}
