using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenrocks : MonoBehaviour {


    Collider collider1;

    void Start() {
        collider1=GetComponent<Collider>();
    }

	void Update () {
        if (gameObject.transform.position.y < 0) {
            collider1.isTrigger = true;
        }   
	}

    void Oncollision() {
        collider1.isTrigger = true;
    }

}
