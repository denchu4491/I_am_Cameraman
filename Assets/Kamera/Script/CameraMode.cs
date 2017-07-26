using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMode : MonoBehaviour {
    public GameObject unitychan;
    bool cameraMode = false;
    private Animator animator;
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    private Vector3 vector3Idlerotation;
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
            if (Input.GetKey("up")) {
                transform.Rotate(new Vector3(-90 * Time.deltaTime, 0f, 0f));
            }
            if (Input.GetKey("down")) {
                transform.Rotate(new Vector3(90 * Time.deltaTime, 0f, 0f));
            }
            if (Input.GetKey("right")) {
                transform.Rotate(new Vector3(0f, 90 * Time.deltaTime, 0f),Space.World);
            }
            if (Input.GetKey("left")) {
                transform.Rotate(new Vector3(0f, -90 * Time.deltaTime, 0f),Space.World);
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
        } 
        else if (GetComponent<PlayerController>().enabled == false) {
            GetComponent<PlayerController>().enabled = true;
            GetComponent<CameraChange>().enabled = true;
            cameraMode = false;
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
            vector3Idlerotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, vector3Idlerotation.y, 0f);
        }
    }

}
