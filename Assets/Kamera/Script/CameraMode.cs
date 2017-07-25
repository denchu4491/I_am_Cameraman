using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMode : MonoBehaviour {
    public GameObject unitychan;
    bool cameraMode = false;
    private Animator animator;
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("z")) {
            ModeCameraChange();
        }
        if (cameraMode) {
            animator.SetBool("Run", false);
            animator.SetBool("Back", false);
        }
	}
    void ModeCameraChange() {
        if (GetComponent<PlayerController>().enabled) {
            GetComponent<PlayerController>().enabled = false;
            GetComponent<CameraChange>().enabled = false;
            cameraMode = true;
            firstPersonCamera.enabled = true;
            thirdPersonCamera.enabled = false;
        } 
        else if (GetComponent<PlayerController>().enabled == false) {
            GetComponent<PlayerController>().enabled = true;
            GetComponent<CameraChange>().enabled = true;
            cameraMode = false;
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
        }
    }

}
