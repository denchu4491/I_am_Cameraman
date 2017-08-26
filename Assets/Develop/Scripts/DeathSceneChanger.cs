using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneChanger : MonoBehaviour {
    public string retrySceneName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("z")) {
            SceneManager.LoadScene(retrySceneName);
        }
        else if (Input.GetKeyDown("x")) {
            SceneManager.LoadScene("Start");
        }
    }
}
