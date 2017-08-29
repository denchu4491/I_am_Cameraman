using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Score_key : MonoBehaviour {

	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z)) {
#if UNITY_EDITOR
            if (File.Exists("Assets/Kamera/Sprite" + "\\takepicture.png")) Texture_hey.Save();
            else EditorUtility.DisplayDialog("写真の保存","今回は写真の撮影に失敗したので、保存は行いません","はい","");

#endif
            SceneManager.LoadScene("Start");
        }
    }
}
