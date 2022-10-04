using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float speed = 30f;
    Vector3 originPosition;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        originPosition = this.transform.position;
     //   GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);

        while (true)
        {
            yield return new WaitForSeconds(10);
            this.transform.position = originPosition;
            this.transform.rotation = Quaternion.identity;
        //    GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
        }
    }

    private void Update()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime); 
    }

}
