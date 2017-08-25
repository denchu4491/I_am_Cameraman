using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBodyCollider : MonoBehaviour {
    PlayerController playerController;
    [System.NonSerialized]public bool Damege,Invincible;
    public float InvincibleTime = 3;
    private float InvincibleCount;
    private Image damegeFlash;
    private CameraMode cameraMode;
    
    // Use this for initialization
    void Awake() {
        playerController = GetComponent<PlayerController>();
        cameraMode = GetComponent<CameraMode>();
        damegeFlash = GameObject.Find("Flash").GetComponent<Image>();
        Invincible = false;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Invincible) {
            playerController.moveController = false;
            damegeFlash.color = new Color(1.0f, 0.0f, 0.0f, 0.8f - InvincibleCount);
            InvincibleCount += Time.deltaTime;
            if (InvincibleTime < InvincibleCount) {
                damegeFlash.color = new Color(1.0f, 1.0f, 1.0f);
                damegeFlash.enabled = false;
                InvincibleCount = 0;
                playerController.moveController = true;
                playerController.animator.SetBool("Damege", false);
                Invincible = false;
            }

        }
	}
    void OnTriggerEnter(Collider collider) {
        Debug.Log(collider.tag);
        if (collider.tag == "EnemyArm" && !Invincible) {
            playerController.Damege();
            playerController.DamegeMove(collider.transform.position,this.transform.position);
            Invincible = true;
            if (cameraMode.boolCameraMode) {
                cameraMode.ModeCameraChange();
                if (cameraMode.takeFlash) {
                    cameraMode.decreaseFlash = 0.8f;
                    cameraMode.takeFlash = false;
                }
            }
            damegeFlash.enabled = true;
            damegeFlash.color = new Color(1.0f, 0.0f, 0.0f,0.8f);
            Debug.Log(playerController.HP);
        } 
    }
    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "EnemyBody" && !Invincible) {
            playerController.Damege();
            playerController.DamegeMove(collision.transform.position, this.transform.position);
            Invincible = true;
            if (cameraMode.boolCameraMode) {
                cameraMode.ModeCameraChange();
                if (cameraMode.takeFlash) {
                    cameraMode.decreaseFlash = 0.8f;
                    cameraMode.takeFlash = false;
                }
            }
            damegeFlash.enabled = true;
            damegeFlash.color = new Color(1.0f, 0.0f, 0.0f, 0.8f);
            Debug.Log(playerController.HP);
        }
    }
}
