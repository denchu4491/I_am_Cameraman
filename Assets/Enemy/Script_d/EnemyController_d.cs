using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_d : MonoBehaviour {
    [System.NonSerialized]
    public Animator animator;
    [System.NonSerialized]
    public Rigidbody rigidbodyE;

    private Vector3 moveDirection, heading;
    public float moveSpeed, rotateSpeed, attackRange = 5.0f;
    private bool canAction, isMovement, isRotation, nowMove;
    private EnemyActionRange_d enemyActionRange;
    public CapsuleCollider attackCollider;

    public readonly static int ANISTS_Idle = Animator.StringToHash("Base Layer.Idle");
    public readonly static int ANISTE_Run = Animator.StringToHash("Base Layer.Run");
    public readonly static int ANISTE_Attack = Animator.StringToHash("Attack");

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Move();

    }

    public void ActionMove(float accel)
    {
        if (accel != 0.0f) {
            moveDirection = new Vector3(transform.forward.x, 0.0f, transform.forward.z) * moveSpeed * accel;
            animator.SetBool("Run", true);
        }
        else {
            moveDirection = Vector3.zero;
            animator.SetBool("Run", false);
        }
    }

    private void Move()
    {
        // 実際の移動は物理演算を行うためにFixedUpdateで行う
        rigidbodyE.velocity = new Vector3(moveDirection.x, rigidbodyE.velocity.y, moveDirection.z);
    }

    public void ActionAttack(string atkname)
    {
        animator.SetTrigger(atkname);
    }

    public void ActionLookUp(GameObject go)
    {
        // 回転制御
        Vector3 angle = go.transform.position - transform.position;
        angle.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(angle), rotateSpeed);
    }
}
