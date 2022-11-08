using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("ForcePositions")]
    [SerializeField] private Transform[] _sails;

    private Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = this.gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        foreach(var item in _sails)
        {
            _rigidBody.AddForceAtPosition(item.transform.forward * 0.1f, item.position, ForceMode.Force);
        }       
    }
}
