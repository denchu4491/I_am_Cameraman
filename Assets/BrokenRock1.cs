using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenRock1 : MonoBehaviour {

    public GameObject bigrock;   //壊れたときの大きい岩
    public GameObject smallrocks; // 壊れて飛び散るいわ
    public float power;

   // Rigidbody rb1;
    Rigidbody rb2;
    Transform children2;
    Collider collider1;
    bool flag = false;

    void Start () {

        bigrock.SetActive(true);
        smallrocks.SetActive(false);
        collider1 = GetComponent<Collider>();
       // rb1 = bigrock.GetComponent<Rigidbody>();
	}

    void Update() {

        if (smallrocks != null) {

            if (smallrocks.activeInHierarchy == true && flag == false) {
                flag = true;
                children2 = smallrocks.GetComponentInChildren<Transform>();
            }
            if (smallrocks.activeInHierarchy == true && flag == true) {
                foreach (Transform child in children2) {
                    if (child.transform.localScale.x < 0|| child.transform.localScale.y < 0||child.transform.localScale.z< 0) ;
                    else child.transform.localScale += new Vector3(-0.0015f, -0.0015f, -0.0015f);
                }
            }
        }
    }
	
     void OnCollisionEnter(Collision collision) {

        Destroy(bigrock);
        Destroy(gameObject,6f);
        smallrocks.SetActive(true);
        rb2=smallrocks.GetComponent<Rigidbody>();
        rb2.AddForce(Random.onUnitSphere*power);
        Destroy(smallrocks, 5f);
        collider1.isTrigger = true;

    }
}
