using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest_d : MonoBehaviour {
    public LayerMask mask;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray();
        }
    }

    void Ray()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        //Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 10.0f))
        {
            Debug.Log(hit.transform.gameObject.tag);
            hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        if (Physics.BoxCast(transform.position, new Vector3(1.0f, 2.0f, 1.0f), transform.forward, out hit, Quaternion.identity, 10.0f))
        {
            hit.collider.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
