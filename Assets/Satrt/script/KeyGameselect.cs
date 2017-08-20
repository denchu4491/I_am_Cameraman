using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyGameselect : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            //Z入力でイージーゲームスタート
            SceneManager.LoadScene("");

        } else if (Input.GetKeyDown(KeyCode.X)) {
            //X入力でノーマルゲームスタート
            SceneManager.LoadScene("");

        } 
    }
}
