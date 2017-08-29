using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class keyStart : MonoBehaviour {

    public new Animation animation;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Z)) {

            SceneManager.LoadScene("GameSelect");
            
           // animation=GetComponent<Animation>();
           // GetComponent<Animator>().SetTrigger("right");
           // Endconfirm("right");

        } else if (Input.GetKeyDown(KeyCode.X)) {
            //X入力でチュートリアルスタート
            SceneManager.LoadScene("tutorial");
        
        }else if (Input.GetKeyDown(KeyCode.C)) {
            //C入力でゲーム終了
            Application.Quit();

        }
	}
  //  private void Endconfirm(string aniname) {
  //      bool fin = false;
  //      while (fin!=true) {
  //          if (!animation.IsPlaying(aniname)) fin=true;
  //      }
  //  }
}
