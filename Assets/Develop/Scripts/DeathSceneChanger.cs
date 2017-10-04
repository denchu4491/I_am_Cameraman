using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DeathSceneChanger : MonoBehaviour {
    public string retrySceneName;
    private PlayerController playercontroller;

    // Use this for initialization
    void Awake() {
        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start () {
        PlayerController.initParam = true;
        CameraMode.score = 0;
        Timer.timeScore=0;
        Timer.gameTime = 1000;
        if (File.Exists(Application.persistentDataPath + "\\takepicture.png")) File.Delete(Application.persistentDataPath + "\\takepicture.png");

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("z")) {
            PlayerController.HP = 2;
            SceneManager.LoadScene(retrySceneName);
        }
        else if (Input.GetKeyDown("x")) {
            playercontroller.deathStop = false;
            SceneManager.LoadScene("Start");
        }
    }
}
