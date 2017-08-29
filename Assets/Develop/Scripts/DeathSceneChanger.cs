using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneChanger : MonoBehaviour {
    public string retrySceneName;
    private PlayerController playercontroller;
    // Use this for initialization
    void Awake() {
        playercontroller = GetComponent<PlayerController>();
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("z")) {
            SceneManager.LoadScene(retrySceneName);
        }
        else if (Input.GetKeyDown("x")) {
            playercontroller.deathStop = false;
            SceneManager.LoadScene("Start");
        }
    }
}
