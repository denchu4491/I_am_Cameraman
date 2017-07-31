using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMode : MonoBehaviour {
    public GameObject unitychan;
    bool cameraMode = false;
    private Animator animator;
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public Canvas canvas;
    private Vector3 vector3Idlerotation;
    public Image flash;
    private float decreaseFlash = 1.0f;
    private bool takeFlash = false;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
    // Update is called once per frame
    void Update () {
        vector3Idlerotation = transform.rotation.eulerAngles;
        //Debug.Log(vector3Idlerotation.x);
        if (Input.GetKeyDown("z")) {
            ModeCameraChange();
            flash.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        if (cameraMode) {
            animator.SetBool("Run", false);
            animator.SetBool("Back", false);
            if(vector3Idlerotation.x > 320 || vector3Idlerotation.x < 55) {
                if (Input.GetKey("up")) {
                    transform.Rotate(new Vector3(-90 * Time.deltaTime, 0f, 0f));
                }
            }
            if(vector3Idlerotation.x > 315 || vector3Idlerotation.x < 40) {
                if (Input.GetKey("down")) {
                    transform.Rotate(new Vector3(90 * Time.deltaTime, 0f, 0f));
                }
            }
            if (Input.GetKey("right")) {
                transform.Rotate(new Vector3(0f, 90 * Time.deltaTime, 0f),Space.World);
            }
            if (Input.GetKey("left")) {
                transform.Rotate(new Vector3(0f, -90 * Time.deltaTime, 0f),Space.World);
            }
            if (Input.GetKeyDown("x")) {
                //TakePicture();
                flash.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                takeFlash = true;
            }
            if (takeFlash) {
                decreaseFlash -= Time.deltaTime;
                flash.color = new Color(1.0f, 1.0f, 1.0f, decreaseFlash);
                if(decreaseFlash < 0) {
                    flash.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    decreaseFlash = 1.0f;
                    takeFlash = false;
                    ModeCameraChange();
                }
            }
        }
	}
    void ModeCameraChange() {
        if (GetComponent<PlayerController>().enabled) {
            GetComponent<PlayerController>().enabled = false;
            GetComponent<CameraChange>().enabled = false;
            cameraMode = true;
            firstPersonCamera.enabled = true;
            thirdPersonCamera.enabled = false;
            canvas.enabled = true;

        } 
        else if (GetComponent<PlayerController>().enabled == false) {
            GetComponent<PlayerController>().enabled = true;
            GetComponent<CameraChange>().enabled = true;
            cameraMode = false;
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
            canvas.enabled = false;
            transform.rotation = Quaternion.Euler(0f, vector3Idlerotation.y, 0f);
        }
    }
    void TakePicture() {
        
    }

}
