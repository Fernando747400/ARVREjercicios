using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{
    public static Cannon Instance;
    [SerializeField] private InputActionReference inputActionReference;
    [SerializeField] private GameObject cannonToRotate;
    [SerializeField] private HandHolder _handHolder;
    [SerializeField] private bool _isGrabbingHolders = false;
    [SerializeField] private int maxProjectiles;
    [SerializeField] private float launchForce;
    public GameObject projectile;
    public GameObject CannonExit;

    public Action<bool> OnGrabHolders;

    public Pooling canonBallPool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        canonBallPool.Preload(projectile, maxProjectiles);
    }

    void Start()
    {
        
        _handHolder.OnGrabbed += HandleGrabHolders;
        inputActionReference.action.performed += ctx => FireCannon();
    }

    private void Update()
    {
        if (_isGrabbingHolders)
        {
            RotateCannon();
        }

        if (Input.GetKeyDown(KeyCode.Space)) FireCannon();
    }

    private void RotateCannon()
    {
        Vector3 aimDirection =  _handHolder.transform.position - cannonToRotate.transform.position;
        cannonToRotate.transform.rotation =  Quaternion.LookRotation(aimDirection);
    }
    
    private void HandleGrabHolders(bool isGrabbing)
    { 
        if (isGrabbing)
        {
            _isGrabbingHolders = true;
        }
        else
        {
            _isGrabbingHolders = false;
        }
        
        OnGrabHolders?.Invoke(_isGrabbingHolders);
    }
    
    
    void FireCannon()
    {
        GameObject canonball = canonBallPool.GetObject(projectile);
        CannonBall canon = canonball.GetComponent<CannonBall>();
        canon.Fire(CannonExit.transform.position, CannonExit.transform.forward, launchForce);
    }

}
