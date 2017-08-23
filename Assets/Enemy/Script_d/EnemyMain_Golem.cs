using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain_Golem : EnemyMain_d{

    private EnemyBodyCollider enemyBodyCol;
    public EnemyActionRange_d enemyActionRange;
    public float attackMoveSpeed;
    private Vector3 firstPosition, rangeSize, rangeCenter, targetPoint;
    private float minX, maxX, minZ, maxZ;
    private bool isRotate;

    public override void Awake()
    {
        base.Awake();

        enemyBodyCol = GetComponentInChildren<EnemyBodyCollider>();
        rangeSize = enemyActionRange.GetComponent<BoxCollider>().size;
        rangeCenter = enemyActionRange.transform.position;
        firstPosition = transform.position;
    }

    public override void Start()
    {
        minX = rangeCenter.x - rangeSize.x / 2;
        maxX = rangeCenter.x + rangeSize.x / 2;
        minZ = rangeCenter.z - rangeSize.z / 2;
        maxZ = rangeCenter.z + rangeSize.z / 2;
    }

    public override void FixedUpdateAI()
    {
        //Debug.Log(string.Format(">>> aiState {0}",aiState));
        switch (aiState)
        {
            case ENEMYAISTS.ACTIONSELECT:
                if (enemyBodyCol.isActionRangeEnter && enemyActionRange.isDetectPlayer)
                {
                    SetAIState(ENEMYAISTS.ATTACKPLAYER, -1.0f);
                }
                else
                {
                    if (!enemyBodyCol.isActionRangeEnter)
                    {
                        targetPoint = firstPosition;
                    }
                    SetAIState(ENEMYAISTS.LOITER, 5.0f);
                }
                enemyCtrl.ActionMove(0.0f);
                break;

            case ENEMYAISTS.LOITER:
                if (enemyBodyCol.isActionRangeEnter && enemyActionRange.isDetectPlayer)
                {
                    enemyCtrl.ActionMove(0.0f);
                    SetAIState(ENEMYAISTS.ATTACKPLAYER, -1.0f);
                }
                else
                {
                    if (!isRotate)
                    {
                        isRotate = true;
                        SetTarget();
                        //Debug.Log("LOITER " + targetPoint);
                        enemyCtrl.ActionLookUp(targetPoint);
                    }
                    if (!enemyCtrl.tryLookUp)
                    {
                        if (!enemyCtrl.ActionMoveToNear(enemyCtrl.target, 1.0f, 1.0f))
                        {
                            isRotate = false;
                            SetAIState(ENEMYAISTS.WAIT, Random.Range(1.0f, 3.0f));
                        }
                    }
                }
                break;

            case ENEMYAISTS.ATTACKPLAYER:
                if (enemyBodyCol.isActionRangeEnter && enemyActionRange.isDetectPlayer)
                {
                    Attack();
                }
                else
                {
                    enemyCtrl.isAttack = false;
                    enemyCtrl.ActionMove(0.0f);
                    SetAIState(ENEMYAISTS.WAIT, 1.0f);
                }
                break;

            case ENEMYAISTS.WAIT:
                enemyCtrl.ActionMove(0.0f);
                break;
        }
    }

    public void Attack()
    {
        enemyCtrl.isAttack = true;
        enemyCtrl.ActionLookUp(player.transform.position);
        if(!enemyCtrl.ActionMoveToNear(player.transform.position, 0.4f, attackMoveSpeed))
        {
            enemyCtrl.ActionMove(0.0f);
        }
    }

    public void SetTarget()
    {
        Vector3 target = Vector3.zero;
        target.x = Random.Range(minX, maxX);
        target.z = Random.Range(minZ, maxZ);
        targetPoint = target;
    }

}
