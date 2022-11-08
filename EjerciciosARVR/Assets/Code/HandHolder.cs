using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class HandHolder : XRBaseInteractable
{
    [Header("Hand Holder")]
    [SerializeField] HandController_xR handController;
    [SerializeField] private InputActionReference inputActionGrab;
    [SerializeField] private InputActionReference inputActionDrop;

    public Action<bool> OnGrabbed;
    private bool _isGrabbing;
    public bool IsGrabbing => _isGrabbing;

    private void Start()
    {
        inputActionGrab.action.performed += ctx => Grab();
        inputActionDrop.action.performed += ctx => Drop();
    }

    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void Grab()
    {
        _isGrabbing = true;
        OnGrabbed?.Invoke(_isGrabbing);
        handController.HandleHandsVisible(false);
    }

    protected virtual void Drop()
    {
        _isGrabbing = false;
        OnGrabbed?.Invoke(_isGrabbing);
        handController.HandleHandsVisible(true);

    }


    private void Update()
    {
        if (IsGrabbing)
        {
            transform.position = GetMidPoint(handController.transform.position, handController.transform.position);   
        }
    }
    
    private Vector3 GetMidPoint(Vector3 p1, Vector3 p2)
    {
        float p3X = (p1.x + p2.x) * .5f;
        float p3Y = (p1.y + p2.y) * .5f;
        float p3Z = (p1.z + p2.z) * .5f;
        Vector3 p3 = new Vector3(p3X, p3Y , p3Z);
        return p3;

    }

    
    
    
}
