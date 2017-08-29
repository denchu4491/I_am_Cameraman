using UnityEngine;
using UnityEngine.UI;

public class Score_time : MonoBehaviour {

    public Text text;
    static public float sum;

    //値を受け取って、タイムのスコアを取得し反映
	void Start () {
        text = GetComponent<Text>();
        sum = Timer.timeScore;
        text.text = sum.ToString("0");
    }

}
