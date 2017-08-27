using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectDefficulty : MonoBehaviour {
    private int stageNumber;
    public RectTransform selectTransform;
    private bool selectMove,desideMove,right,left,select,deside;
    private float waitTime;
    private string desideText;
    public Image flash;
    public Text operationText;
    public string[] sceneName = new string[3];
    public Image[] selectImage = new Image[3];
    public Text[] explanatoryText = new Text[3];
    // Use this for initialization
    void Awake() {
        stageNumber = 2;
        select = true;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (select) {
            if (Input.GetKeyDown("right") && stageNumber < 3 && !selectMove) {
                stageNumber++;
                selectMove = true;
                right = true;
            }
            if (Input.GetKeyDown("left") && stageNumber > 1 && !selectMove) {
                stageNumber--;
                selectMove = true;
                left = true;
            }
            if (Input.GetKeyDown("x") && !selectMove) {
                deside = true;
                desideMove = true;
                select = false;
                Deside(stageNumber);
                explanatoryText[stageNumber - 1].enabled = true;
                operationText.enabled = false;
            }
        }

        if(deside && !desideMove){
            if (Input.GetKeyDown("x")) {
                Flash();
                SceneManager.LoadScene(sceneName[stageNumber -1]);
            }
            if (Input.GetKeyDown("z")) {
                select = true;
                deside = false;
                explanatoryText[stageNumber - 1].enabled = false;
                operationText.enabled = true;
                for (int i = 0; i < 3; i++) {
                    if (stageNumber - 1 != i) {
                        selectImage[i].enabled = true;
                    }
                }
                selectTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                stageNumber = 2;
            }

        }

        
    }
    void FixedUpdate() {
        if (selectMove) {
            waitTime += Time.fixedDeltaTime;
            if (right) {
                Serect(1);
            } else if (left) {
                Serect(2);
            }
            if (waitTime >= 1.0f) {
                selectMove = false;
                right = false;
                left = false;
                waitTime = 0;
            }
        }

        if (desideMove) {
            waitTime += Time.fixedDeltaTime;
            DesideMove();
            if(waitTime >= 1.0f) {
                desideMove = false;
                waitTime = 0;
            }
        }

    }

    void Serect(int direction) {
        if(direction == 1) {
            selectTransform.localPosition -= new Vector3(1200.0f, 0.0f, 0.0f) * Time.fixedDeltaTime;
        }
        else if(direction == 2) {
            selectTransform.localPosition += new Vector3(1200.0f, 0.0f, 0.0f) * Time.fixedDeltaTime;
        }
    }

    void Deside(int number) {
        for(int i = 0;i < 3; i++) {
            if(number - 1 != i) {
                selectImage[i].enabled = false;
            }
        }
    }

    void DesideMove() {
        selectTransform.localPosition -= new Vector3(400.0f, 0.0f, 0.0f) * Time.fixedDeltaTime;
    }

    void Flash() {
        flash.enabled = true;
        flash.color = new Color(1.0f,1.0f,1.0f,1.0f);
    }

}
