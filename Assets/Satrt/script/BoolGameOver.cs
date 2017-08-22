using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BoolGameOver : MonoBehaviour {

    public Animator animator;
    public new Animation animation;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {

            SceneManager.LoadScene("");
        } else if (Input.GetKeyDown(KeyCode.X)) {
            GameoverText.action();
            animator = GameoverText.ani;
            Endconfirm("disappear");
            SceneManager.LoadScene("Gameselect");
        }
    }


    private void Endconfirm(string aniname) {
        bool fin = false;
        while (!fin) {
            if (!animation.IsPlaying(aniname)) fin = true;
        }
    }
}
