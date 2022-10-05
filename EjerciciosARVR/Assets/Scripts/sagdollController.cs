using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sagdollController : MonoBehaviour
{
    [Header("esto esta prendido cuando hay animacion")]
    public Animator animator;
    public Collider mainCollider;
    public Rigidbody mainRigidbody;

    [Header("esto esta apagado cuando hay animacion")]
    public List<Rigidbody> rigidbodies;
    public List<Collider> colliders;

    public event Action GotHitEvent;

    void Start()
    {
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            if (item.gameObject != this.gameObject)
            {
                rigidbodies.Add(item);
                item.isKinematic = true;
                item.useGravity = false;
            }
        }

        foreach (var item in GetComponentsInChildren<Collider>())
        {
            if (item.gameObject != this.gameObject)
            {
                colliders.Add(item);
                item.enabled = false;
            }
        }
        mainCollider = GetComponent<Collider>();

        mainRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    bool value = true;
    public void Toggle()
    {

        value = !value;

        foreach (var item in colliders)
        {
            item.enabled = !value;
        }

        foreach (var item in rigidbodies)
        {
            item.isKinematic = value;
            item.useGravity = !value;
        }

        
        mainCollider.enabled = value;
        animator.enabled = value;
        mainRigidbody.isKinematic = true;
    }

    private void GotHit()
    {
        GotHitEvent?.Invoke();
    }
   
}
