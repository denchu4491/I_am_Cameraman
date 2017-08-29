using UnityEngine;
using UnityEngine.UI;

public class Score_picture : MonoBehaviour {

    public Text text;
    static public float picture_p;
    public float per=60;

    //値を受けっとって、写真のスコアを取得し反映
    void Start() {
        float pictureScore = CameraMode.score;
        
        text = GetComponent<Text>();
        picture_p = pictureScore * per;
        text.text = picture_p.ToString("#");
    }

   
}
