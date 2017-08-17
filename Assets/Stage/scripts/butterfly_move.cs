using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butterfly_move : MonoBehaviour {

    private GameObject bf;
    public float dgree=500;
    public float y_r = 1;
    public float dgree_r = 10;
    public float move_distans = 10;



    // Use this for initialization
    void Start () {
        bf = GameObject.Find("butterfly");

    }
	
	// Update is called once per frame
	void Update () {
        var x = transform.localPosition.x;
        var z = transform.localPosition.z;
        x = 5 * Mathf.Sin(Time.time);
        z = 5 * Mathf.Cos(Time.time);
        y_r += (Time.deltaTime * dgree_r) ;
        //if (y_r > 360) y_r = 0;
        bf.transform.position += new Vector3( (x+move_distans) / dgree, 0, z / dgree);
        transform.rotation = Quaternion.Euler(0, 1+y_r, 0);
    }
}
