using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenrocks : MonoBehaviour {

   
    

	void Update () {
        Transform tf = GetComponent<Transform>();
        Debug.Log(tf.position.y);
        Debug.Log(gameObject.name);
        if (tf.position.y <= -5) {
            Destroy(gameObject);
        }
	}
}
