using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    Rigidbody rb;
    CameraMode cameraMode;
    public GameObject gameOverDesign;
    public int initHP = 2;
    public float movespeed = 0.2f;
    public float jumpPower = 20;
    public float jumpTime = 1.4f;
    private float waitDeathAnimation;
    private bool death;
    private float jumpCooldownTime;
    float moveZ;
    public Image helthImage;
    public Text helthPointText;
    [System.NonSerialized]public bool deathStop,isSliding,isJump,isBack,isRun,moveController = true,isGround,isJumping = false;
    Vector3 jumpCheck,moveNormal;
    [System.NonSerialized]public Animator animator;
    RaycastHit slideHit;

    public static bool checkPointEnabled = false;
    public static string checkPointSceneName = "";
    public static string checkPointLabelName = "";
    public static int HP = 0;
    public static bool initParam = true;


    // Use this for initialization
    void Awake() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (initParam)
        {
            HP = initHP;
            initParam = false;
        }

        if (checkPointEnabled)
        {
            StageTrigger_Link[] triggerList = GameObject.Find("StageTrigger").GetComponentsInChildren<StageTrigger_Link>();
            foreach(StageTrigger_Link trigger in triggerList)
            {
                if(trigger.labelName == checkPointLabelName)
                {
                    Transform spot = trigger.transform.Find("Spot");
                    transform.position = spot.position;
                    transform.rotation = spot.rotation;
                }
            }
        }

        helthPointText.text = string.Format("{0}", HP);
    }

    void Start () {
    }
	// Update is called once per frame
	void Update () {
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

        if(Physics.Raycast(transform.position + new Vector3(0.0f,0.3f,0.0f),Vector3.down,out slideHit)) {
            if(Vector3.Angle(slideHit.normal,Vector3.up) > 65.0f) {
                isSliding = true;
            } else {
                isSliding = false;
            }
        }

        if (moveController && !deathStop) {
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

        if (death) {
            waitDeathAnimation += Time.deltaTime;
            if(waitDeathAnimation >= 1.3f) {
                animator.speed = 0.0f;
                waitDeathAnimation = 0.0f;
                gameOverDesign.SetActive(true);
                GetComponent<DeathSceneChanger>().enabled = true;
                GetComponent<CameraMode>().enabled = false;
                GetComponent<PlayerController>().enabled = false;
            }
        }

    }

    void FixedUpdate() {
        Move();
        if (isJump) {
            Jump();
            isJump = false;
        }
        if(!isJump && isSliding ) {
            Slide();
        }
    }
    
    void Move() {
        Vector3 _pos = (transform.forward * moveZ) * movespeed;
        rb.velocity = new Vector3(_pos.x, rb.velocity.y, _pos.z);
    }

    void Jump() {
        rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
    }
    void Slide() {
        Debug.Log("ZURETERU");
        moveNormal = slideHit.normal;
        rb.velocity = new Vector3(moveNormal.x,rb.velocity.y,moveNormal.z);
    }

    public void DamegeMove(Vector3 playerPosition,Vector3 targetPosition) {
        Debug.Log("tobeeeeeeeeeeeeeeeeee");
        float damegeMoveX = targetPosition.x - playerPosition.x;
        float damegeMoveZ = targetPosition.z - playerPosition.z;
        rb.AddForce(damegeMoveX * 200.0f, 0.0f, damegeMoveZ * 200.0f,ForceMode.VelocityChange);
    }

    public void Damege() {
        HP--;
        Debug.Log("itai");
        helthPointText.text = string.Format("{0}", HP);
        if(HP == 0) {
            Death();
        }
    }

    void Death() {
        death = true;
        animator.SetBool("Death",true);
        deathStop = true;
        Debug.Log("owarei");
    }

}
