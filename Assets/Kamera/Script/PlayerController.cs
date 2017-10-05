using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    Rigidbody rb;
    CameraMode cameraMode;
    public GameObject gameOverDesign;
    public DeathSceneChanger deathSceneChanger;
    public int initHP = 3;
    public float movespeed = 0.2f;
    public float jumpPower = 20;
    public float jumpTime = 1.4f;
    private float waitDeathAnimation;
    private bool death;
    private float jumpCooldownTime;
    float moveZ;
    public Image helthImage;
    public Text helthPointText;
    [System.NonSerialized]public bool deathStop = false,isSliding,isJump,isBack,isRun,moveController = true,isGround,isJumping = false;
    Vector3 jumpCheck,moveNormal;
    [System.NonSerialized]public Animator animator;
    RaycastHit slideHit;
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public AudioSource bgmSource;
    public AudioClip deathClip;
    public Image mapImage;

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
    }

    void Start () {
        helthPointText.text = string.Format("{0}", HP);
    }

	// Update is called once per frame
	void Update () {
        moveZ = 0;
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
            if (Input.GetKeyDown("v")) {
                if (mapImage.enabled) {
                    mapImage.enabled = false;
                } else {
                    mapImage.enabled = true;
                }

            }

            if (Input.GetKey("up")) {
                animator.SetBool("Run", true);
                if (!isRun) {
                    audioSource.clip = audioClip[0];
                    audioSource.Play();
                }
                isRun = true;
                moveZ += 1;
            } else {
                isRun = false;
            }
            if (Input.GetKey("down")) {
                animator.SetBool("Back", true);
                moveZ -= 1 * 0.5f;
                if (!isBack) {
                    audioSource.clip = audioClip[1];
                    audioSource.Play();
                }
                isBack = true;
            }
            else {
                isBack = false;
            }
            if(!isBack && !isRun) {
                audioSource.Stop();
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
                if (!isJump) {
                    audioSource.clip = audioClip[0];
                    audioSource.Play();
                }
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
                if (isRun || isBack) {
                    audioSource.Play();
                }
            }
            if(jumpCooldownTime > 0.5) {
                audioSource.Stop();
            }
        }

        if (death) {
            waitDeathAnimation += Time.deltaTime;
            if(waitDeathAnimation >= 1.3f) {
                animator.speed = 0.0f;
                waitDeathAnimation = 0.0f;
                gameOverDesign.SetActive(true);
                deathSceneChanger.enabled = true;
                audioSource.Stop();
                bgmSource.clip = deathClip;
                bgmSource.Play();
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
        moveNormal = slideHit.normal;
        rb.velocity = new Vector3(moveNormal.x,rb.velocity.y,moveNormal.z);
    }

    public void DamegeMove(Vector3 playerPosition,Vector3 targetPosition) {
        /*
        float damegeMoveX = targetPosition.x - playerPosition.x;
        float damegeMoveZ = targetPosition.z - playerPosition.z;
        */

        Vector3 direction = playerPosition - targetPosition;
        direction.Normalize();
        Debug.Log(direction);
        //rb.AddForce(damegeMoveX * 50.0f, 5.0f, damegeMoveZ * 50.0f,ForceMode.VelocityChange);
        rb.AddForce(direction.x * 100.0f, 5.0f, direction.z * 100.0f, ForceMode.VelocityChange);
    }

    public void Damege() {
        HP--;
        helthPointText.text = string.Format("{0}", HP);
        if(HP <= 0) {
            Death();
        }
    }

    void Death() {
        death = true;
        animator.SetBool("Death",true);
        deathStop = true;
    }

}
