using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMAP
{
    public Transform VRTarget;
    public Transform RigTarget;
    public Vector3 TrackingPositionOffset;
    public Vector3 TrackingRotationOffset;

    public void Map()
    {
        RigTarget.position = VRTarget.TransformPoint(TrackingPositionOffset);
        RigTarget.rotation = VRTarget.rotation * Quaternion.Euler(TrackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public VRMAP HeadMap;
    public VRMAP LeftHandMap;
    public VRMAP RightHandMap;

    public Transform HeadConstrain;
    public Vector3 HeadOffsetBody;

    private void Start()
    {
        HeadOffsetBody = transform.position - HeadConstrain.position;
    }

    private void LateUpdate()
    {
        transform.position = HeadConstrain.position + HeadOffsetBody;
        transform.forward = Vector3.ProjectOnPlane(HeadConstrain.forward, Vector3.up).normalized;

        HeadMap.Map();
        LeftHandMap.Map();
        RightHandMap.Map();
    }
}
