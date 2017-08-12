using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
    Rigidbody rb;
    PlayerController playerController;
    public float gravity = 3;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }
	// Update is called once per frame
	void FixedUpdate () {
        if (!playerController.isJumping && !playerController.isGround) {
            rb.velocity = new Vector3(rb.velocity.x, gravity * -1.0f, rb.velocity.z);
        }
	}
}
