using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Animator _animator;
    [SerializeField] private sagdollController _ragdollController;
    [SerializeField] private Rigidbody _hipsRigidBody;
    [SerializeField] private int _hitsToEndure;

    private int _totalHits = 0;

    private void Awake()
    {
        AddEvent();
    }

    private void Start()
    {
        ResetAllTriggers();
        _animator.SetTrigger("Idle");
    }

    private void GotCollision(GameObject collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        Debug.Log("Total hits " + _totalHits);
        _totalHits++;
        if (_totalHits < _hitsToEndure)
        {
            GetHit();
        }
        else
        {
            Debug.Log("Die");
            Die();
        }
    }

    private void ResetAllTriggers()
    {
        _animator.ResetTrigger("Idle");
        _animator.ResetTrigger("Hit");
        _animator.ResetTrigger("Punch");
    }

    private void GetHit()
    {
        ResetAllTriggers();
        _animator.SetTrigger("Hit");
    }

    private void Punch()
    {
        ResetAllTriggers();
        _animator.SetTrigger("Punch");
    }

    private void Die()
    {
        ResetAllTriggers();
        RemoveEvents();
        _hipsRigidBody.constraints = RigidbodyConstraints.None;
        _ragdollController.Toggle();
    }

    private void AddEvent()
    {
        foreach (var collider in GetComponentsInChildren<Collider>())
        {
            collider.AddComponent<ColliderEvent>();
        }

        foreach (var Event in GetComponentsInChildren<ColliderEvent>())
        {
            Event.OnCollisionEnterEvent += GotCollision;
        }
        _ragdollController.GotHitEvent += Punch;
    }

    private void RemoveEvents()
    {
        foreach (var Event in GetComponentsInChildren<ColliderEvent>())
        {
            Event.OnCollisionEnterEvent -= GotCollision;
        }

        foreach (var collider in GetComponentsInChildren<Collider>())
        {
            Destroy(collider.GetComponent<ColliderEvent>());
        }
    }
}
