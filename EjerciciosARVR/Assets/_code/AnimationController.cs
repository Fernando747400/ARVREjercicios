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
    [SerializeField] private int _hitsToEndure = 5;
    [SerializeField ] private float _hitCoolDown = 2f;
    [SerializeField] private float _ragdollTime = 5f;
    [SerializeField] private GameObject _modelHips;

    private bool _isRagdolling = false;
    private int _totalHits = 0;
    private float _inCoolDown = 0f;
    private float _ragdollTimer = 0f;

    private Vector3 _initialHipsPos;
    private Vector3 _initialHipsRot;

    private void Awake()
    {
        AddEvent();
        _initialHipsPos = _modelHips.transform.localPosition;
        _initialHipsRot = _modelHips.transform.localEulerAngles;
    }

    private void Start()
    {
        ResetAllTriggers();
        _animator.SetTrigger("Idle");
    }

    private void FixedUpdate()
    {
        _inCoolDown += Time.deltaTime;
        if (_isRagdolling)
        {
            _ragdollTimer += Time.deltaTime;
            if (_ragdollTimer >= _ragdollTime)
            {
                _ragdollTimer = 0f;
                Revive();
            }
        }
    }

    private void GotCollision(GameObject collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        Debug.Log("Total hits " + _totalHits);
        if (_inCoolDown < _hitCoolDown) return;
        if (_isRagdolling) return;
        _inCoolDown = 0f;
        _totalHits++;
        if (_totalHits < _hitsToEndure)
        {
            GetHit();
        }
        else
        {
            Debug.Log("Die");
            Die();
            _isRagdolling = true;
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
        _hipsRigidBody.constraints = RigidbodyConstraints.None;
        _ragdollController.Toggle();
    }
    
    private void Revive()
    {
        _isRagdolling = false;
        _totalHits = 0;
        _ragdollController.Toggle();
        _modelHips.transform.position = _initialHipsPos;
        _modelHips.transform.rotation = Quaternion.Euler(_initialHipsRot);
        ResetAllTriggers();
        _animator.SetTrigger("Idle");
        _hipsRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //_hipsRigidBody.constraints = RigidbodyConstraints.FreezeRotationZ;
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
