using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour {


    public GameObject Rock;
    MeshCollider meshcollider;


	void Update () {
        if (Input.GetKeyDown(KeyCode.T)) {
            GameObject obj=Rock;
            Vector3 t3= GameObject.Find("Button_throw").transform.position;
            obj.transform.position = t3;
            obj.AddComponent<Rigidbody>();
            obj.GetComponent<MeshCollider>().enabled = true;
            Instantiate(obj);
        }	
	}
}
