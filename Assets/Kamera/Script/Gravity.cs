using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
    Rigidbody rb;
    PlayerController playerController;
    Vector3 JumpCheck;
    public float gravity = 3;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }
	// Update is called once per frame
	void FixedUpdate () {
        JumpCheck = transform.position + transform.up * -0.4f;
        if (!playerController.isJumping && !playerController.isGround) {
            Debug.Log("aaaaaaaaa");
            rb.velocity = new Vector3(rb.velocity.x, gravity * -1.0f, rb.velocity.z);
        }
	}
}
