using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float gameTime;
    private bool playingGame;
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
    }
}
