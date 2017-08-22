using UnityEngine;
using UnityEngine.UI;

public class Score_total : MonoBehaviour {

    private float total;
    public Text text;
	
    //トータルのスコアを反映
    //※StartではうまくいかなかったのでUpdateで行います
	void Update () {
        total = Score_picture.picture_p + Score_time.sum;
        text = GetComponent<Text>();
        text.text = total.ToString("#");

    }

  

}

   
