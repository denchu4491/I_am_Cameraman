﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    public AudioClip bgm;


	
	void Start () {
        GetComponent<AudioSource>().PlayOneShot(bgm);
    }
	
	
	void Update () {
        
	}
}
