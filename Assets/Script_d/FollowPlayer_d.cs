using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer_d : MonoBehaviour {
    public Transform target;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        transform.position = new Vector3(target.localPosition.x, target.localPosition.y + 0.5f, target.localPosition.z + 1.4f);
        transform.rotation = target.rotation;
        */

        transform.rotation = target.rotation;
        Vector3 playerForward = Vector3.Scale(target.forward, new Vector3(1, 0, 1)).normalized;
        transform.position = target.position + playerForward;
	}
}
