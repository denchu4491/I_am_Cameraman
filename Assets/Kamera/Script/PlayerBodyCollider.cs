using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyCollider : MonoBehaviour {
    PlayerController playerController;
    // Use this for initialization
    void Awake() {
        playerController = GetComponent<PlayerController>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider collider) {
        Debug.Log("aaa");
        if(collider.tag == "EnemyArm") {
            playerController.HP -= 1;
            Debug.Log(playerController.HP);
        } 
    }
}
