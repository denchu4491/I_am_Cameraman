using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameScreen : MonoBehaviour {
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad() {
        Screen.SetResolution(1920, 1200, false);
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
