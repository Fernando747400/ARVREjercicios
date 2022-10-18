using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyancy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _underwaterDrag = 4f;
    [SerializeField] private float _underwaterAngularDrag = 2f;
    [SerializeField] private float _airDrag = 0f;
    [SerializeField] private float _airAngularDrag = 0f;
    [SerializeField] private float _floatingForce = 0f;
    [SerializeField] private float _waterHeight = 0f;

    private Rigidbody _rigidBody;
    private bool _isUnderwater;
    private bool _isInside;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float deltaHeight = transform.position.y - _waterHeight;

        if (!_isInside) return;
        if(deltaHeight < 0)
        {
            _rigidBody.AddForceAtPosition(Vector3.up * _floatingForce * Mathf.Abs(deltaHeight), transform.position, ForceMode.Force);
            
            if (!_isUnderwater)
            {
                _isUnderwater = true;
                SwitchState(_isUnderwater);
            }
        }
        else
        {
            if (_isUnderwater)
            {
                _isUnderwater = false;
                SwitchState(_isUnderwater);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            _isInside = true;
            _waterHeight = other.transform.position.y;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        _isInside = false;
        _waterHeight = 0f;
    }

    private void SwitchState(bool IsUnderwater)
    {
        _rigidBody.drag = IsUnderwater ? _underwaterDrag : _airDrag;
        _rigidBody.angularDrag = IsUnderwater ? _underwaterAngularDrag : _airAngularDrag;
    }
}
