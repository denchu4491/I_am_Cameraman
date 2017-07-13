using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Animator anim = GetComponent<Animator>();
        if (Input.GetKey(KeyCode.Space)) {
            anim.SetBool("is_jumping", true);
        }
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Locomotion.Jump")) {
            anim.SetBool("is_jumping", false);
        }
	}
}
