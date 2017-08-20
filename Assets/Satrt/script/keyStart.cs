using UnityEngine;
using UnityEngine.SceneManagement;

public class keyStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z)) {
            //Z入力でゲームスタート
            SceneManager.LoadScene("GameSelect");

        }else if (Input.GetKeyDown(KeyCode.X)) {
            //X入力でチュートリアルスタート
            SceneManager.LoadScene("Tutorial");
        
        }else if (Input.GetKeyDown(KeyCode.C)) {
            //C入力でゲーム終了
            Application.Quit();

        }
	}
}
