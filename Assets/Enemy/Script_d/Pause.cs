using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    private bool isStopTime = false;

	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameStop();
        }
	}

    void GameStop()
    {
        if (!isStopTime)
        {
            Time.timeScale = 0;
            Debug.Log("STOP");
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("RESTART");
        }
        isStopTime = !isStopTime;
    }
}
