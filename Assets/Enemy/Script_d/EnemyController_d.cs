using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_d : MonoBehaviour {

    [System.NonSerialized] public Animator animator;
    [System.NonSerialized] public Rigidbody rigidbodyE;
    [System.NonSerialized] public EnemyActionRange_d enemyActionRange;
    [System.NonSerialized] public Vector3 target;
    [System.NonSerialized] public bool tryLookUp;

    public float moveSpeed, rotateSpeed;
    private Collider attackCollider;
    private Vector3 moveDirection, heading;
    private Quaternion rotationPrev;
    private float attackTimeStart, attackTimeLength;

    public readonly static int ANISTS_Idle = Animator.StringToHash("Base Layer.Idle");
    public readonly static int ANISTE_Run = Animator.StringToHash("Base Layer.Run");
    public readonly static int ANISTE_Attack = Animator.StringToHash("Attack");

    public void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbodyE = GetComponent<Rigidbody>();
        enemyActionRange = GetComponentInChildren<EnemyActionRange_d>();
        attackCollider = transform.Find("Collider_Attack").gameObject.GetComponent<Collider>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        // 攻撃判定の終了の確認
        if (attackCollider.enabled)
        {
            float time = Time.fixedTime - attackTimeStart;
            if (time > attackTimeLength)
            {
                attackCollider.enabled = false;
            }
        }

        // ターゲットへの方向転換
        if (tryLookUp)
        {
            LookUp(target);
        }

        // 移動
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

    public bool ActionMoveToNear(Vector3 go, float near)
    {
        heading = go - transform.position;
        if (heading.sqrMagnitude > near * near)
        {
            ActionMove(1.0f);
            return true;
        }
        return false;
    }

    private void LookUp(Vector3 go)
    {
        // 回転制御
        Vector3 angle = go - transform.position;
        angle.y = 0;
        rotationPrev = transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(angle), rotateSpeed);

        // 回転終了チェック
        if(transform.rotation == rotationPrev)
        {
            tryLookUp = false;
        }
    }

    public void ActionAttack(string atkname, float atktime)
    {
        animator.SetTrigger(atkname);
        attackCollider.enabled = true;
        attackTimeStart = Time.fixedTime;
        attackTimeLength = atktime;
    }

    public void ActionLookUp(Vector3 t)
    {
        target = t;
        tryLookUp = true;
    }
}
