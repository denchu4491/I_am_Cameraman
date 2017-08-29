using UnityEngine;
using UnityEngine.UI;

public class Score_total : MonoBehaviour {

    private float total;
    public Text text;
    bool flag;
	
    //トータルのスコアを反映
    //※StartではうまくいかなかったのでUpdateで行います
    void Start() {
        flag = false;
        text = GetComponent<Text>();
    }

	void Update () {
        if (flag == false) {
            flag = true;
            total = Score_picture.picture_p + Score_time.sum;
            text.text = total.ToString("0");
            RankingScore.Judge(total);
        }
    }

  

}

   
