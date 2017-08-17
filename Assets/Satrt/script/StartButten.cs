using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

 [RequireComponent(typeof(Button))]

public class StartButten : UIBehaviour {
    
    protected override void Start() {
        base.Start();

        GetComponent<Button>().onClick.AddListener(OnClick);
    }


    void OnClick() {
        //[GameSelect]シーンに移動
        SceneManager.LoadScene("GameSelect");
    }
}
