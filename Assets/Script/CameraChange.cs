using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C)) {
            Debug.Log("aa");
            Debug.Log(firstPersonCamera.enabled + "firstPersonCamera");
            Debug.Log(thirdPersonCamera.enabled + "thirdPersonCamera");
            if (firstPersonCamera.enabled == false) {
                Debug.Log("ii");
                thirdPersonCamera.enabled = false;
                firstPersonCamera.enabled = true;
                firstPersonCamera.tag = "MainCamera";
                thirdPersonCamera.tag = "SubCamera";
            }
            else if (thirdPersonCamera.enabled == false) {
                Debug.Log("uu");
                firstPersonCamera.enabled = false;
                thirdPersonCamera.enabled = true;
                thirdPersonCamera.tag = "MainCamera";
                firstPersonCamera.tag = "SubCamera";
            }
        
        }

	}
}
