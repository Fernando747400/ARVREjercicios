using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEvent : MonoBehaviour
{
    public event Action<GameObject> OnCollisionEnterEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCollisionEnterEvent?.Invoke(collision.gameObject);
        }       
    }
}
