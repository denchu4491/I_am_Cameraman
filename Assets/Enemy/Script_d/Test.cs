using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    private Rigidbody rigidbodyE;
    public float speed;
    private Transform rayStart;

    void Awake()
    {
        rigidbodyE = GetComponent<Rigidbody>();
        rayStart = transform.Find("RayStart");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        RaycastHit hit;
        if (!Physics.Raycast(rayStart.position, transform.forward, out hit, 2.0f))
        {
            // 移動
            rigidbodyE.velocity = new Vector3(transform.forward.x * speed, rigidbodyE.velocity.y, transform.forward.z * speed);
        }
        else
        {
            Debug.Log("RayHit");
            Debug.Log(Vector3.Angle(hit.normal, Vector3.up));
        }
    }
}
