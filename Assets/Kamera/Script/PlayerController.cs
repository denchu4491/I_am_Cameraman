using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody rb;
    public float movespeed = 0.2f;
    public float jumpPower = 20;
    float moveX;
    float moveZ;
    bool isJump;
    private Vector3 velosity = Vector3.zero;
    private bool isRun;
    Vector3 JumpCheck;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        Animator motion = GetComponent<Animator>();
        AnimatorStateInfo state = motion.GetCurrentAnimatorStateInfo(0);
        moveX = 0;
        moveZ = 0;
        isRun = false;
        JumpCheck = transform.position + transform.up * -0.4f;
        if (Input.GetKey("up")) {
            //moveX += 1;
            moveZ += 1;
        }
        if (Input.GetKey("down")) {
            //moveX -= 1;
            moveZ -= 1;
        }
        if (Input.GetKey("right")) {
            transform.Rotate(new Vector3(0f, 90 * Time.deltaTime, 0f));
        }
        if (Input.GetKey("left")) {
            transform.Rotate(new Vector3(0f, -90 * Time.deltaTime, 0f));
        }
        if (Input.GetKey("space")) {
            if (Physics.CheckSphere(JumpCheck, 0.3f)) {
                isJump = true;
            }
        }
        
    }
    void FixedUpdate() {
        
        Move();
        if (isJump) {
            Jump();
            isJump = false;
        }
    }
    void Move() {
        Vector3 _pos = (transform.forward * moveZ + transform.right * moveX) * movespeed;
        rb.velocity = new Vector3(_pos.x, rb.velocity.y, _pos.z);
    }
    void Jump() {
        rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
    }
}
