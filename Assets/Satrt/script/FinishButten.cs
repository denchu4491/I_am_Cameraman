using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]

public class FinishButten : UIBehaviour {

    protected override void Start() {
        base.Start();


        //on clickを呼び出し
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick() {
        //ゲームを終了する 上はunity本体が死んじゃう。下はたぶんなる
        //EditorApplication.Exit(0);
        Application.Quit();

            }

    
}
