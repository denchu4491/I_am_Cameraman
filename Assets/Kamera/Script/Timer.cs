using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float gameTime;
    public static float timeScore;
    public GameObject GameOver;
    private bool playingGame;
    private PlayerController playercontroller;
    void Awake() {
        
    }
	// Use this for initialization
	void Start () {
        GameStart();
	}
	
	// Update is called once per frame
	void Update () {
        if (playingGame) {
            gameTime -= Time.deltaTime;
            GetComponent<Text>().text = ((int)gameTime).ToString();
            if (gameTime <= 0) {
                gameTime = 0;
                GameFinish();
                playingGame = false;
            }
        }
	}
    void GameStart() {
        playingGame = true;
    }
    void GameFinish() {
        Debug.Log("END");
        //シーン飛ばしそう
        GameOver.SetActive(true);
        GameObject.Find("Player").GetComponent<DeathSceneChanger>().enabled = true;
        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();
        playercontroller.deathStop = true;
    }
}
