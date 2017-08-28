using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public string selestScene;
    public GameObject SceneChangerMessage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    /*void OnCollisionStay(Collision collision) {
        if(collision.collider.tag == "Player") {
            Debug.Log("aaaaaaaaaaaaaaaaaaa");
            SceneChangerMessage.SetActive(true);
            if (Input.GetKeyDown("x")) {
                SceneManager.LoadScene(selestScene);
            }
        }
    }

    void OnCollisionExit(Collision collision) {
        if(collision.collider.tag == "Player") {
            SceneChangerMessage.SetActive(false);
        }
    }*/

    void OnTriggerStay(Collider collision) {
        if (collision.tag == "Player") {
            Debug.Log("aaaaaaaaaaaaaaaaaaa");
            SceneChangerMessage.SetActive(true);
            if (Input.GetKeyDown("x")) {
                SceneManager.LoadScene(selestScene);
            }
        }
    }

    void OnTriggerExit(Collider collision) {
        if (collision.tag == "Player") {
            SceneChangerMessage.SetActive(false);
        }
    }
}
