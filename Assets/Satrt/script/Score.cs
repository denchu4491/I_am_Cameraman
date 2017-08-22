using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text text1;
    public Text text2;
    public Text text3;

    public float time_p;
    public float picture_p;
    public float total;

    // Use this for initialization
    void Start () {
        text1 = GetComponent<Text>();
        text2 = GetComponent<Text>();
        text3 = GetComponent<Text>();
        time_p = 1111;
        picture_p = 1111;
        total = time_p + picture_p;

        text1.text = time_p.ToString("#");
        text2.text = picture_p.ToString("#");
        text3.text = total.ToString("#");
    }
	
	
}
