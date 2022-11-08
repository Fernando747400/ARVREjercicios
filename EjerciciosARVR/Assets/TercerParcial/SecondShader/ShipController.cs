using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("ForcePositions")]
    [SerializeField] private Transform[] _sails;
    [SerializeField] private float _windForce;

    [SerializeField] private Transform _rudder;
    [SerializeField] private float _rudderForce;

    [SerializeField] private GameObject _rudderWheel;

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
    private void Update()
    {
        ReadInput();
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

    private void ReadInput()
    {
        if (_rudderWheel == null) return;

        if (Input.GetKeyDown(KeyCode.A)) 
        {
            HeadingState = Heading.Left;
            RotateWheel(new Vector3(0,0,45));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            HeadingState = Heading.Right;
            RotateWheel(new Vector3(0, 0, -45));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            HeadingState = Heading.Forward;
            RotateWheel(Vector3.zero);
        }
    }

    private void RotateWheel(Vector3 degrees)
    {
        iTween.RotateTo(_rudderWheel, iTween.Hash("rotation", degrees, "islocal", true, "time", 4f));
    }
}
