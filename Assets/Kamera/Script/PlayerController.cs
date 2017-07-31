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
    bool isBack;
    private Vector3 velosity = Vector3.zero;
    private bool isRun;
    Vector3 JumpCheck;
    private Animator animator;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        moveX = 0;
        moveZ = 0;
        isRun = false;
        isBack = false;
        animator.SetBool("Run", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Back", false);
        JumpCheck = transform.position + transform.up * -0.4f;
        if (Input.GetKey("up")) {
            animator.SetBool("Run", true);
            moveZ += 1;
        }
        if (Input.GetKey("down")) {
            animator.SetBool("Back", true);
            moveZ -= 1 * 0.5f;
            isBack = true;
        }
        if (Input.GetKey("right")) {
            transform.Rotate(new Vector3(0f, 90 * Time.deltaTime, 0f));
        }
        if (Input.GetKey("left")) {
            transform.Rotate(new Vector3(0f, -90 * Time.deltaTime, 0f));
        }
        if (Input.GetKey("space") && isBack == false) {
            if (Physics.CheckSphere(JumpCheck, 0.3f)) {
                isJump = true;
                animator.SetBool("Jump", true);
                if (isRun == false) {
                    animator.SetBool("IdleJump", true);
                }
            }
        }
        //animator.SetBool("Run", false);
        
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
