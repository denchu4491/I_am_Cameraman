using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public string selestScene;
    public GameObject SceneChangerMessage;
    public Timer timer;

    // Use this for initialization
    void Awake() {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerStay(Collider collision) {
        if (collision.tag == "Player") {
            SceneChangerMessage.SetActive(true);
            if (Input.GetKeyDown("x")) {
                timer.timeScore = timer.gameTime / 10;
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
