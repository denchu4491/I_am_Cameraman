using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BoolkeyStart : MonoBehaviour {

    public new Animation animation;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {

            SceneManager.LoadScene("GameSelect");
           

        } else if (Input.GetKeyDown(KeyCode.X)) {
            //X入力でチュートリアルスタート
            SceneManager.LoadScene("Tutorial");

        } else if (Input.GetKeyDown(KeyCode.C)) {
            //C入力でゲーム終了
            Application.Quit();

        }
    }
   
   
}
