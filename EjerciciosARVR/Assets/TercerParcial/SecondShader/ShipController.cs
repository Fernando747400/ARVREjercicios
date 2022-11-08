using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("ForcePositions")]
    [SerializeField] private Transform[] _sails;
    [SerializeField] private float _windForce;

    [SerializeField] private Transform _rudder;
    [SerializeField] private float _rudderForce;

    public enum Heading
    {
        Forward,
        Left,
        Right
    }

    public Heading HeadingState;

    private Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = this.gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        foreach(var item in _sails)
        {
            _rigidBody.AddForceAtPosition(item.transform.forward * _windForce, item.position, ForceMode.Force);
        }

        MoveRudder();
    }

    public void MoveRudder()
    {
        switch (HeadingState)
        {
            case Heading.Forward:
                //_rigidBody.AddForceAtPosition(_rudder.position * _rudderForce, _rudder.position, ForceMode.Force);
                break;
            case Heading.Left:
                _rigidBody.AddForceAtPosition(_rudder.right * _rudderForce, _rudder.position, ForceMode.Force);
                break;
            case Heading.Right:
                _rigidBody.AddForceAtPosition(-_rudder.right * _rudderForce, _rudder.position, ForceMode.Force);
                break;
        }
    }
}
