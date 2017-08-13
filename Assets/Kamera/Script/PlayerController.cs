using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody rb;
    CameraMode cameraMode;
    public int HP = 2;
    public float movespeed = 0.2f;
    public float jumpPower = 20;
    public float jumpTime = 1.2f;
    private float jumpCooldownTime;
    float moveX,moveZ;
    [System.NonSerialized]public bool isJump,isBack,isRun,moveController = true,isGround,isJumping;
    Vector3 jumpCheck;
    private Animator animator;
    // Use this for initialization
    void Awake() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Start () {
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
        jumpCheck = transform.position + transform.up * -0.4f;

        if (Physics.CheckSphere(jumpCheck, 0.3f)) {
            isGround = true;
        }
        else {
            isGround = false;
        }

        if (moveController) {
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
        }

        if (Input.GetKeyDown("space") && isBack == false && !isJumping) {
            if (isGround) {
                isJump = true;
                isJumping = true;
                animator.SetBool("Jump", true);
                if (isRun == false) {
                    animator.SetBool("IdleJump", true);
                }
            }
        }
        if (isJumping) {
            jumpCooldownTime += Time.deltaTime;
            if(jumpCooldownTime > jumpTime) {
                jumpCooldownTime = 0;
                isJumping = false;
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
