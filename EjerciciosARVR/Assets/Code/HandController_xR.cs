using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController_xR : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer handMeshRenderer;
    
    public void HandleHandsVisible(bool handsVisible)
    {
        handMeshRenderer.enabled = handsVisible;
    }
}
