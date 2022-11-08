using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class CannonBall : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _collider;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();

        ResetValues();
    }

    void Start()
    {
        StartCoroutine(DespawnBall());
    }

    void Activate()
    {
        _meshRenderer.enabled = true;
        _collider.enabled = true;
        _rb.isKinematic = false;
    }

    public void Fire(Vector3 origin, Vector3 direction, float magnitude)
    {
        transform.position = origin;
        transform.eulerAngles = Vector3.zero;
        Activate();
        _rb.AddForce((-1) * direction.normalized * magnitude);
    }

    IEnumerator DespawnBall()
    {
        yield return new WaitForSeconds(15);
        ResetValues();
        Cannon.Instance.canonBallPool.RecicleObject(Cannon.Instance.projectile, this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject.GetComponent<BouyancyComplex>());
            Destroy(other.gameObject.GetComponent<ShipController>());
            ResetValues();
            Cannon.Instance.canonBallPool.RecicleObject(Cannon.Instance.projectile, this.gameObject);
        }
    }

    void ResetValues()
    {
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
        _meshRenderer.enabled = false;
        _collider.enabled = false;
    }

}
