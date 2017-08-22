using UnityEngine;
using UnityEngine.UI;

public class Score_picture : MonoBehaviour {

    public Text text;
    static public float picture_p;
    public float per=1;

    //値を受けっとって、写真のスコアを取得し反映
    void Start() {
        text = GetComponent<Text>();
        picture_p = 100 * per;
        text.text = picture_p.ToString("#");
    }

   
}
