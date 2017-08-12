﻿using System.Collections;
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
    private float decreaseFlash = 0.8f;
    private bool takeFlash = false;
    private PlayerController playerController;
    //public LayerMask mask;
    // Use this for initialization
    void Awake() {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Start () {
	}
    // Update is called once per frame
    void Update () {
        vector3Idlerotation = firstPersonCamera.transform.rotation.eulerAngles;
        //Debug.Log(vector3Idlerotation.x);
        if (Input.GetKeyDown("z") && (!takeFlash)) {
            ModeCameraChange();
            flash.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        if (cameraMode) {
            animator.SetBool("Run", false);
            animator.SetBool("Back", false);
            if(vector3Idlerotation.x > 320 || vector3Idlerotation.x < 55) {
                if (Input.GetKey("up")) {
                    firstPersonCamera.transform.Rotate(new Vector3(-90 * Time.deltaTime, 0f, 0f));
                }
            }
            if(vector3Idlerotation.x > 315 || vector3Idlerotation.x < 40) {
                if (Input.GetKey("down")) {
                    firstPersonCamera.transform.Rotate(new Vector3(90 * Time.deltaTime, 0f, 0f));
                }
            }
            if (Input.GetKey("right")) {
                transform.Rotate(new Vector3(0f, 90 * Time.deltaTime, 0f),Space.World);
            }
            if (Input.GetKey("left")) {
                transform.Rotate(new Vector3(0f, -90 * Time.deltaTime, 0f),Space.World);
            }
            if (Input.GetKeyDown("x") && (!takeFlash)) {
                TakePicture();
                takeFlash = true;
            }
            if (takeFlash) {
                decreaseFlash -= Time.deltaTime;
                flash.color = new Color(1.0f, 1.0f, 1.0f, decreaseFlash);
                if(decreaseFlash < 0) {
                    flash.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    decreaseFlash = 0.8f;
                    takeFlash = false;
                    ModeCameraChange();
                }
            }
        }
	}
    void ModeCameraChange() {
        if (playerController.moveController) {
            playerController.moveController = false;
            cameraMode = true;
            firstPersonCamera.enabled = true;
            thirdPersonCamera.enabled = false;
            canvas.enabled = true;

        } 
        else if (!playerController.moveController) {
            playerController.moveController = true;
            cameraMode = false;
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
            canvas.enabled = false;
            transform.rotation = Quaternion.Euler(0f, vector3Idlerotation.y, 0f);
            firstPersonCamera.transform.rotation = Quaternion.Euler(0f, vector3Idlerotation.y, 0f);
        }
    }
    void TakePicture() {
        Ray ray = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);
        Debug.Log("rrrrrr");
        
        RaycastHit hitObj;
        if (Physics.Raycast(ray,out hitObj, 100.0f)) {
            if (hitObj.collider.tag == "EnemyBody") {
                float distance = Vector3.Distance(hitObj.transform.position, transform.position);
                Debug.Log(distance);
                Debug.Log(hitObj.collider.tag);
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 10.0f);
            }
        }
    }
}
