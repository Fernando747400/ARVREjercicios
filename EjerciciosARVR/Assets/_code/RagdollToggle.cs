using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class RagdollToggle : MonoBehaviour
{

    private Rigidbody _mainRB;
    private Collider _mainCC;
    private Animator _animator;
    private List<Rigidbody> _rigidBodiesList = new List<Rigidbody>();
    private List<Collider> _collidersList = new List<Collider>();

    private void Start()
    {
        Prepare();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    private void Toggle()
    {

    }


    private void Prepare()
    {
        _mainRB = GetComponent<Rigidbody>();
        _mainCC = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
        
        foreach(var rb in GetComponentsInChildren<Rigidbody>())
        {
            if(rb != this.gameObject)
            {
                _rigidBodiesList.Add(rb);
                rb.isKinematic = true;
                rb.useGravity = false;
            }
            
        }

        foreach(var coll in GetComponentsInChildren<Collider>())
        {
            _collidersList.Add(coll);
            coll.enabled = false;
        }
    }

    //Con animacion prender capsula y rg principal, no kinematico. 
    //Sin Animacion kinematico. Apagado. Kinematico. 
}
