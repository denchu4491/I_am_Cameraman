using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterMove : MonoBehaviour {
    public Transform player;
    void Awake() {
        player = GameObject.Find("Player").transform;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position;
	}
}
