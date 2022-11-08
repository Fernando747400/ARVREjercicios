using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _wavesHeight = 5f;
    [SerializeField] private float _wavesFrequency = 0.01f;
    [SerializeField] private float _wavesSpeed = 0.02f;
    [SerializeField] private Transform _ocean;

    private Material _oceanMaterial;
    private Texture2D _wavesDisplacement;

    private void SetVariables()
    {
        _oceanMaterial = _ocean.GetComponent<Renderer>().sharedMaterial;
        _wavesDisplacement = (Texture2D)_oceanMaterial.GetTexture("_WaveDisplacement");
    }
    
    public float WaterHeightAtPosition(Vector3 position)
    {
        return _ocean.position.y + _wavesDisplacement.GetPixelBilinear(position.x * _wavesFrequency, position.z * Time.time * _wavesSpeed).g * _wavesHeight * _ocean.localScale.x;
    }

    private void OnValidate()
    {
        if (!_oceanMaterial) SetVariables();
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        _oceanMaterial.SetFloat("_WavesFrequency", _wavesFrequency);
        _oceanMaterial.SetFloat("_WavesSpeed", _wavesSpeed);
        _oceanMaterial.SetFloat("_WavesHeight", _wavesHeight);
    }
}

