using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour {
    public Text[] descriptionText = new Text[5];
    public GameObject TextPanel;
    private int descriptionNumber;
    private bool explanation;
    // Use this for initialization
    void Awake() {
        explanation = true;
        descriptionNumber = 0;
    }

    void Start () {
        SetDescriptionText(descriptionNumber);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Return) && explanation) {
            descriptionNumber += 1;
            if (descriptionNumber == 5) {
                TextPanel.SetActive(false);
                DeleteDescriprionText(descriptionNumber - 1);
                explanation = false;
            } else {
                SetDescriptionText(descriptionNumber);
            }
        }
    }
    void SetDescriptionText(int n) {
        descriptionText[n].enabled = true;
        if(n != 0) {
            DeleteDescriprionText(n-1);
        }
    }
    void DeleteDescriprionText(int n) {
        descriptionText[n].enabled = false;
    }
}
