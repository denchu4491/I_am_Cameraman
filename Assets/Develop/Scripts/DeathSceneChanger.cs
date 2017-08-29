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
        playercontroller = GetComponent<PlayerController>();
    }

    void Start () {
        CameraMode.score = 0;
        Timer.timeScore=0;
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
