using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour {
    public Text[] descriptionText = new Text[8];
    public Text enterText;
    public Image TextPanel;
    private float waitTime;
    private int descriptionNumber;
    private bool explanation,getPicture;
    CameraMode cameraMode;
    // Use this for initialization
    void Awake() {
        explanation = true;
        descriptionNumber = 0;
        cameraMode = GameObject.Find("Player").GetComponent<CameraMode>();
    }

    void Start () {
        SetDescriptionText(descriptionNumber);
	}
	
	// Update is called once per frame
	void Update () {
        waitTime += Time.deltaTime;
        if (explanation) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                if (descriptionNumber < 7) {
                    descriptionNumber++;
                    Debug.Log(descriptionNumber);
                    if (!getPicture) {
                        waitTime = 0;
                    }
                    if (descriptionNumber == 7) {
                        TextPanel.enabled = false;
                        enterText.enabled = false;
                        DeleteDescriprionText(descriptionNumber - 1);
                    } else {
                        SetDescriptionText(descriptionNumber);
                    }
                }

                if (getPicture && descriptionNumber >= 7) {
                    descriptionNumber++;
                    if (descriptionNumber == 9) {
                        DeleteDescriprionText(descriptionNumber - 1);
                        explanation = false;
                        TextPanel.enabled = false;
                        enterText.enabled = false;
                    } else {
                        SetDescriptionText(descriptionNumber);
                        waitTime = 0;
                    }
                }
            }

            if (cameraMode.takeFlash && descriptionNumber == 7) {
                getPicture = true;
                TextPanel.enabled = true;
                enterText.enabled = true;
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
