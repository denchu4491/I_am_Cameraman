using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class Score_key : MonoBehaviour {

	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z)) {
            if (File.Exists(Application.persistentDataPath + "\\takepicture.png")) Texture_hey.Save();
            else EditorUtility.DisplayDialog("写真の保存","今回は写真の撮影に失敗したので、保存は行いません","はい","");
            SceneManager.LoadScene("Start");
        }	
	}
}
