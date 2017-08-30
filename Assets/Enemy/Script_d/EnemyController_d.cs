using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_d : MonoBehaviour {

    [System.NonSerialized] public Animator animator;
    [System.NonSerialized] public Rigidbody rigidbodyE;
    [System.NonSerialized] public Vector3 target;
    [System.NonSerialized] public bool tryLookUp;

    public float moveSpeed, rotateSpeed = 0.1f, localGravityScale = 10.0f, sphereRadius = 0.3f;
    public Collider attackCollider;
    private Vector3 moveDirection;
    private Quaternion rotationPrev;
    private float attackTimeStart, attackTimeLength;
    private Transform groundCheck;
    //private Transform rayStart;
    private bool isSetLocalGravity;

    public readonly static int ANISTS_Idle = Animator.StringToHash("Base Layer.Idle");
    public readonly static int ANISTE_Run = Animator.StringToHash("Base Layer.Run");
    public readonly static int ANISTE_Attack = Animator.StringToHash("Attack");

    public void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbodyE = GetComponent<Rigidbody>();
        rigidbodyE.useGravity = false;
        groundCheck = transform.Find("GroundCheck");
        //rayStart = transform.Find("RayStart");
    }

    void FixedUpdate()
    {
        // 接地チェック
        if (!Physics.CheckSphere(groundCheck.position, sphereRadius))
        {
            //Debug.Log("hoge");
            // 浮いてる時の重力
            isSetLocalGravity = true;
            SetLocalGravity();
        }
        else
        {
            //通常の重力
            if (isSetLocalGravity)
            {
                isSetLocalGravity = false;
                rigidbodyE.velocity = new Vector3(rigidbodyE.velocity.x, 0, rigidbodyE.velocity.z);
            }
            rigidbodyE.AddForce(Physics.gravity, ForceMode.Acceleration);
        }

        // 攻撃判定の終了チェック
        if (attackCollider != null)
        {
            if (attackCollider.enabled)
            {
                float time = Time.fixedTime - attackTimeStart;
                if (time > attackTimeLength)
                {
                    attackCollider.enabled = false;
                }
            }
        }

        // ターゲットへの方向転換
        if (tryLookUp)
        {
            LookUp(target);
        }
        /*
        // 障害物チェック
        if (isAttack || !Physics.Raycast(rayStart.position, transform.forward, 2.0f))
        {
        */
            // 移動
            Move();
        //}
    }

    void SetLocalGravity()
    {
        rigidbodyE.velocity = Vector3.down * localGravityScale;
    }

    public void ActionMove(float accel)
    {
        if (accel != 0.0f) {
            moveDirection = new Vector3(transform.forward.x, 0.0f, transform.forward.z) * accel;
            animator.SetBool("Run", true);
        }
        else {
            moveDirection = Vector3.zero;
            animator.SetBool("Run", false);
        }
    }

    private void Move()
    {
        rigidbodyE.velocity = new Vector3(moveDirection.x, rigidbodyE.velocity.y, moveDirection.z);
    }

    public bool ActionMoveToNear(Vector3 go, float near, float speed)
    {
        Vector3 from = transform.position;
        go.y = 0.0f;
        from.y = 0.0f;
        Vector3 heading = go - from;
        if (heading.sqrMagnitude > near * near)
        {
            ActionMove((speed > 0.0f) ? speed : moveSpeed);
            return true;
        }
        return false;
    }

    private void LookUp(Vector3 go)
    {
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

    public void ActionAttack(string atkname)
    {
        animator.SetTrigger(atkname);
    }

    public void ActionLookUp(Vector3 t)
    {
        target = t;
        tryLookUp = true;
    }
}
