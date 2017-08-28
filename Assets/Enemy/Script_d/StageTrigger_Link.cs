using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTrigger_Link : MonoBehaviour {

    public string labelName = "";
    public string jumpSceneName;
    public string jumpLabelName;
    public float jumpDelayTime = 0.0f;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag != "Player")
        {
            return;
        }

        Jump();
    }

    public void Jump()
    {
        PlayerController.checkPointEnabled = true;
        PlayerController.checkPointLabelName = jumpLabelName;
        PlayerController.checkPointSceneName = jumpSceneName;

        Invoke("JumpWork", jumpDelayTime);
    }

    void JumpWork()
    {
        SceneManager.LoadScene(jumpSceneName);
    }
}
