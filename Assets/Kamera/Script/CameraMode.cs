using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraMode : MonoBehaviour {
    public GameObject unitychan;
    [System.NonSerialized]public bool boolCameraMode = false, takeFlash = false,zoomUp;
    [System.NonSerialized]public float decreaseFlash = 0.8f,scoreFlashColor;
    public float targetDistance;
    public static float score;
    private Animator animator;
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    private Image flash, cameraFrame;
    public SphereCollider shutterSoundCollider;
    private Vector3 vector3Idlerotation;
    private PlayerController playerController;
    private PlayerBodyCollider boolInvincible;
    public AudioClip shutterSound;
    public AudioSource audioSource;

    //public LayerMask mask;
    // Use this for initialization
    void Awake() {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        boolInvincible = GetComponent<PlayerBodyCollider>();
        flash = GameObject.Find("Flash").GetComponent<Image>();
        cameraFrame = GameObject.Find("CameraFrame").GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shutterSound;
    }

    void Start () {
        
	}
    // Update is called once per frame
    void Update () {
        vector3Idlerotation = firstPersonCamera.transform.rotation.eulerAngles;
        //Debug.Log(vector3Idlerotation.x);
        if (Input.GetKeyDown("z") && (!takeFlash) && !boolInvincible.Invincible && !playerController.deathStop) {
            ModeCameraChange();
            flash.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        if (boolCameraMode) {
            animator.SetBool("Run", false);
            animator.SetBool("Back", false);

            if(vector3Idlerotation.x > 320 || vector3Idlerotation.x < 55) {
                if (Input.GetKey("up")) {
                    if (!zoomUp) {
                        firstPersonCamera.transform.Rotate(new Vector3(-45 * Time.deltaTime, 0f, 0f));
                    } else {
                        firstPersonCamera.transform.Rotate(new Vector3(-22.5f * Time.deltaTime, 0f, 0f));
                    }
                }
            }

            if(vector3Idlerotation.x > 315 || vector3Idlerotation.x < 40) {
                if (Input.GetKey("down")) {
                    if (!zoomUp) {
                        firstPersonCamera.transform.Rotate(new Vector3(45 * Time.deltaTime, 0f, 0f));
                    } else {
                        firstPersonCamera.transform.Rotate(new Vector3(22.5f * Time.deltaTime, 0f, 0f));
                    }
                }
            }

            if (Input.GetKey("right")) {
                if (!zoomUp) {
                    transform.Rotate(new Vector3(0f, 45 * Time.deltaTime, 0f), Space.World);
                } else {
                    transform.Rotate(new Vector3(0f, 22.5f * Time.deltaTime, 0f), Space.World);
                }
            }

            if (Input.GetKey("left")) {
                if (!zoomUp) {
                    transform.Rotate(new Vector3(0f, -45f * Time.deltaTime, 0f), Space.World);
                } else {
                    transform.Rotate(new Vector3(0f, -22.5f * Time.deltaTime, 0f), Space.World);
                }
            }

            if (Input.GetKeyDown("c")) {
                if(firstPersonCamera.fieldOfView == 60) {
                    CameraZoomUp();
                } else {
                    CameraZoomOut();
                }
            }
            if (Input.GetKeyDown("x") && (!takeFlash)) {
                TakePicture();
                takeFlash = true;
            }
            if (takeFlash) {
                decreaseFlash -= Time.deltaTime;
                if(score < 30.0f) {
                    flash.color = new Color(1.0f, 1.0f, 1.0f, decreaseFlash);
                    if (decreaseFlash < 0) {
                        flash.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                        decreaseFlash = 0.8f;
                        takeFlash = false;
                        ModeCameraChange();
                    }
                }
                
                else if(score >= 30.0f) {
                    flash.color = new Color(1.0f, 1.0f, 0.0f, decreaseFlash);
                    if (decreaseFlash < 0) {
                        flash.color = new Color(1.0f, 1.0f, 0.0f, 0.0f);
                        decreaseFlash = 0.8f;
                        takeFlash = false;
                        ModeCameraChange();
                    }
                }
            }
        }
	}

    public void ModeCameraChange() {
        if (playerController.moveController) {
            playerController.moveController = false;
            boolCameraMode = true;
            firstPersonCamera.enabled = true;
            thirdPersonCamera.enabled = false;
            flash.enabled = true;
            cameraFrame.enabled = true;
            playerController.helthImage.enabled = false;
            playerController.helthPointText.enabled = false;
        } 
        else if (!playerController.moveController) {
            playerController.moveController = true;
            boolCameraMode = false;
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
            flash.enabled = false;
            cameraFrame.enabled = false;
            shutterSoundCollider.enabled = false;
            playerController.helthImage.enabled = true;
            playerController.helthPointText.enabled = true;
            if (zoomUp) {
                CameraZoomOut();
            }
            transform.rotation = Quaternion.Euler(0f, vector3Idlerotation.y, 0f);
            firstPersonCamera.transform.rotation = Quaternion.Euler(0f, vector3Idlerotation.y, 0f);
        }
    }

    void CameraZoomUp() {
        if(firstPersonCamera.fieldOfView == 60) {
            firstPersonCamera.fieldOfView = 30;
            zoomUp = true;
        } 
    }
    void CameraZoomOut() {
        firstPersonCamera.fieldOfView = 60;
        zoomUp = false;
    }

    void CaptchaScreen() {
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture renderTexture = new RenderTexture(screenShot.width, screenShot.height, 24);
        RenderTexture prev = firstPersonCamera.targetTexture;
        firstPersonCamera.targetTexture = renderTexture;
        firstPersonCamera.Render();
        firstPersonCamera.targetTexture = prev;
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply();

        byte[] bytes = screenShot.EncodeToPNG();
        Object.Destroy(screenShot);

        string filename = "takepicture.png";
        File.WriteAllBytes(Application.persistentDataPath + "/" + filename, bytes);

    }

    void TakePicture() {
        audioSource.Play();
        shutterSoundCollider.enabled = true;

        CaptchaScreen();

        Ray ray = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);
        
        RaycastHit hitObj;
        score = 0;
        if (Physics.SphereCast(firstPersonCamera.transform.position + firstPersonCamera.transform.right * 1.0f, 1.0f, firstPersonCamera.transform.forward, out hitObj, 100.0f)) {
            if (hitObj.collider.tag == "EnemyBody") {
                float distance = Vector3.Distance(hitObj.transform.position, transform.position);
                if (distance > 5 && zoomUp) {
                    distance -= 5;
                    if (distance <= 5) {
                        distance = 7.0f;
                    }
                }
                if (targetDistance - distance < 0) {
                    score = 40 + (targetDistance - distance);
                } else if (targetDistance - distance > 0) {
                    score = 20;
                }
                Debug.Log(distance);
                Debug.Log(score);
            }
        }

        if (Physics.SphereCast(firstPersonCamera.transform.position + firstPersonCamera.transform.right * -1.0f, 1.0f, firstPersonCamera.transform.forward, out hitObj, 100.0f)) {
            if (hitObj.collider.tag == "EnemyBody") {
                float distance = Vector3.Distance(hitObj.transform.position, transform.position);
                if (distance > 5 && zoomUp) {
                    distance -= 5;
                    if(distance <= 5) {
                        distance = 7.0f;
                    }
                }
                if (targetDistance - distance < 0) {
                    score = 40 + (targetDistance - distance);
                } else if (targetDistance - distance > 0) {
                    score = 20;
                }
                Debug.Log(distance);
                Debug.Log(score);
            }
        }

        if (Physics.SphereCast(ray,0.5f,out hitObj, 100.0f)) {
            if (hitObj.collider.tag == "EnemyBody") {
                float distance = Vector3.Distance(hitObj.transform.position, transform.position);
                if (distance > 5 && zoomUp) {
                    distance -= 5;
                    if (distance <= 5) {
                        distance = 7.0f;
                    }
                }
                if (targetDistance - distance < 0) {
                    score = 50 + (targetDistance - distance);
                }
                else if(targetDistance - distance > 0){
                    score = 20;
                }
                Debug.Log(distance);
                Debug.Log(score);
            }
        }
        if(score <= 0) {
            score = 0;
        }
    }

}
