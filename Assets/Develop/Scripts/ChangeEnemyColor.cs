using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemyColor : MonoBehaviour {
    public Renderer enemyObjectRenderer;
	// Use this for initialization
	void Start () {
        //enemyObjectRenderer = GameObject.Find("TargetGolem").GetComponent<Renderer>();
        enemyObjectRenderer.material.color = new Color(1.0f,0.0f,0.0f,1.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
