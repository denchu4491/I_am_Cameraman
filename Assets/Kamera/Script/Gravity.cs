using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
    Rigidbody rb;
    public float gravity = 3;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity += new Vector3(0.0f,gravity * -1.0f,0.0f);
	}
}
