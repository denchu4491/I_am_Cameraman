using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_d : MonoBehaviour {

    public float moveSpeed, rotateSpeed;
    protected bool canAction,isMovement, isRotation;
    protected EnemyActionRange_d enemyActionRange;
    protected Rigidbody rigidbodyE;

    protected virtual void Awake()
    {
        canAction = true;
        isMovement = false;
        isRotation = false;

        enemyActionRange = GetComponentInChildren<EnemyActionRange_d>();
        rigidbodyE = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	protected virtual void Start () {

	}
	
	// Update is called once per frame
	protected virtual void Update () {

	}

    protected virtual void FixedUpdate()
    {
        if (canAction)
        {
            // アクション範囲内にプレイヤーがいる場合
            if (enemyActionRange.isDetectPlayer)
            {
                isMovement = true;
                isRotation = true;

                if (isMovement && Vector3.Distance(enemyActionRange.lookTarget.position, transform.position) > 3.0f)
                {
                    Move();
                }

                // 回転制御
                if (isRotation && enemyActionRange.lookTarget != null)
                {
                    Vector3 angle = enemyActionRange.lookTarget.position - transform.position;
                    angle.y = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(angle), rotateSpeed);
                }
            }
        }
    }

    protected virtual void Move()
    {
        /*
        Vector3 vect = enemyActionRange.lookTarget.position - transform.position;
        rigidbody_d.velocity = vect.normalized * moveSpeed;
        */
        Vector3 forward = new Vector3(transform.forward.x, 0.0f, transform.forward.z);
        rigidbodyE.velocity = forward * moveSpeed;
    }

}
