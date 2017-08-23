using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_d : MonoBehaviour {

    public float moveSpeed, rotateSpeed, waitTimeLength, attackRange = 5.0f;
    public CapsuleCollider attackCollider;
    private bool canAction, isRotation, isMove, isAttack;
    private EnemyActionRange_d enemyActionRange;
    private Rigidbody rigidbodyE;
    private Animator animator;
    private Vector3 heading, angle, targetPoint;
    private float waitTimeStart, attackTimeLength, attackTimeStart;

    protected virtual void Awake()
    {
        canAction = true;
        isRotation = false;

        enemyActionRange = GetComponentInChildren<EnemyActionRange_d>();
        rigidbodyE = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

	// Use this for initialization
	protected virtual void Start () {
        attackTimeLength = 1.0f;
        waitTimeLength = 5.0f;
    }
	
	// Update is called once per frame
	protected virtual void Update () {

        if (isMove)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
	}

    protected virtual void FixedUpdate()
    {
        isMove = false;

        // 攻撃終了の確認
        if (isAttack)
        {
            float aTime = Time.fixedTime - attackTimeStart;
            if (aTime > attackTimeLength)
            {
                isAttack = false;
                attackCollider.enabled = false;
                waitTimeStart = Time.fixedTime;
            }
        }

        // 待機時間終了の確認
        if (!canAction && !isAttack)
        {
            float wTime = Time.fixedTime - waitTimeStart;
            if (wTime > waitTimeLength)
            {
                canAction = true;
                isRotation = true;
                targetPoint = Vector3.zero;
            }
        }
        else if (canAction)
        {
            // アクション範囲内にプレイヤーがいる場合
            if (enemyActionRange.isDetectPlayer)
            {
                if(targetPoint == Vector3.zero)
                {
                    isRotation = true;
                }

                // ターゲット地点の設定
                if (isRotation)
                {
                    isRotation = false;
                    //targetPoint = enemyActionRange.lookTarget.position;
                    angle = targetPoint - transform.position;
                    angle.y = 0;
                }
            }

            // ターゲット地点が指定されているか
            if (targetPoint != Vector3.zero)
            {
                /*
                // 回転制御
                if (enemyActionRange.lookTarget != null)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(angle), rotateSpeed);
                }
                */
                heading = targetPoint - transform.position;

                // 移動
                if (heading.sqrMagnitude > attackRange * attackRange)
                {
                    isMove = true;
                    Move();
                }

                // 攻撃処理
                if (heading.sqrMagnitude <= attackRange * attackRange && !isAttack)
                {
                    Attack("Attack_Bite");
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

    public void Attack(string atkname)
    {
        animator.SetTrigger(atkname);
        isAttack = true;
        canAction = false;
        attackTimeStart = Time.fixedTime;
        attackCollider.enabled = true;
    }

}
