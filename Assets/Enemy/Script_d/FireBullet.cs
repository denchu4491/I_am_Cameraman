using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour {

    public float lifeTime = 3.0f;
    public float speed = 10.0f;
    public float rotateVt = 360.0f;
    //public float angle;
    //private float fireTime;
    private Vector3 posTarget;
    private Quaternion homingRotate;

    [System.NonSerialized] public Transform ownwer;
    [System.NonSerialized] public GameObject targetObject;
    [System.NonSerialized] public Rigidbody rigidbodyB;

    void Awake()
    {
        targetObject = GameObject.Find("Player");
        rigidbodyB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        posTarget = targetObject.transform.position + new Vector3(0.0f, 1.5f, 0.0f);
        homingRotate = Quaternion.LookRotation(posTarget - transform.position);

        //fireTime = Time.fixedTime;
        Destroy(this.gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger || other.tag == "Enemy" || other.tag == "EnemyBody" || other.tag == "EnemyArm" || other.tag == "Breakable")
        {
            return;
        }
        
        rigidbodyB.Sleep();
        this.enabled = false;

        //Destroy(this.gameObject);
    }

    void Update()
    {
        transform.Rotate(Time.deltaTime * rotateVt, 0.0f, 0.0f);
    }

    void FixedUpdate()
    {
        Vector3 vecMove = (homingRotate * Vector3.forward) * speed;
        //rigidbodyB.velocity = Quaternion.Euler(0.0f, 0.0f, angle) * vecMove;
        rigidbodyB.velocity = vecMove;
    }
}
