using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingScore : MonoBehaviour {


    static Text text;	
	void Start () {
        text = GetComponent<Text>();
        
    }

    static public void Judge(float total_) {
       // Debug.Log(total_);
        if (total_ >= 3500) {
            text.text = "S";
        } else if (total_ >= 3000) {
            text.text = "A";
        } else if (total_>=2000) {
            text.text = "B";
        }else if (total_ > 1000) {
            text.text = "C";
        }else text.text = "D";
    } 
	
	
}
