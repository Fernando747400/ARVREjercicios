using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdoll : MonoBehaviour
{
    public GameObject ragdoll;
    public GameObject reference;

    public List<ConfigurableJoint> configurableJoints;
    public List<Transform> referenceJoints;

    public List<Quaternion> referenecQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        configurableJoints = new List<ConfigurableJoint>();
        referenceJoints = new List<Transform>();
        referenecQuaternion = new List<Quaternion>();
        foreach (var item in ragdoll.GetComponentsInChildren<ConfigurableJoint>())
        {
            configurableJoints.Add(item);
        }
        foreach (var item in reference.GetComponentsInChildren<Transform>())
        {
            for (int i = 0; i < configurableJoints.Count; i++)
            {
                if (item.name == configurableJoints[i].name)
                {
                    referenceJoints.Add(item);
                    referenecQuaternion.Add(item.transform.localRotation);
                    break;
                }
            }
        }
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < configurableJoints.Count; i++)
        {
            configurableJoints[i].targetRotation = Quaternion.Inverse(referenceJoints[i].localRotation) * referenecQuaternion[i]; 
        }
        
    }
}
