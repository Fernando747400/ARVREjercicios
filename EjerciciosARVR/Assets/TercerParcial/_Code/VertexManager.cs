using UnityEngine;

public class VertexManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask _layermask;
    
    public Vector3 SendWaterCast(Transform floatingTransfrom)
    {        
        if (Physics.Raycast(floatingTransfrom.position, floatingTransfrom.up , out RaycastHit hitOne, 1000f, _layermask))
        {
            Debug.Log("Hitted with ray one");
            return GetVector(hitOne);
        } else if (Physics.Raycast(floatingTransfrom.position, -floatingTransfrom.up, out RaycastHit hitTwo, 1000f, _layermask))
        {
            Debug.Log("Hitted with ray two");
            return GetVector(hitTwo);
        } else
        {
            Debug.Log("No water hit"); return Vector3.zero;
        }
    }

    private Vector3 GetVector(RaycastHit hit)
    {

        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null)
        {
            Debug.Log("No mesh collider"); return Vector3.zero;
        }

        Mesh WaterMesh = meshCollider.GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = WaterMesh.vertices;
        int[] triangles = WaterMesh.triangles;

        Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
        Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
        Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];

        Transform hitTransform = hit.collider.transform;

        p0 = hitTransform.TransformPoint(p0);
        p1 = hitTransform.TransformPoint(p1);
        p2 = hitTransform.TransformPoint(p2);

        Debug.DrawLine(p0, p1, Color.red);
        Debug.DrawLine(p1, p2, Color.red);
        Debug.DrawLine(p2, p0, Color.red);

        Debug.Log("The water position is " + p0);
        
        return p0;
    }
}
