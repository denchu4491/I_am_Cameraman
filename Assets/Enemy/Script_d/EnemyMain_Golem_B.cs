using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain_Golem_B : EnemyMain_d {

    private EnemyActionRange_d enemyActionRange;
    public float attackMoveSpeed, attackWaitTime;
    private Vector3 firstPosition, rangeSize, rangeCenter, targetPoint;
    private float minX, maxX, minZ, maxZ;
    private bool isRotate;

    public override void Awake()
    {
        base.Awake();

        enemyActionRange = GetComponentInParent<EnemyActionRange_d>();
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
        switch (aiState)
        {
            case ENEMYAISTS.ACTIONSELECT:
                isRotate = false;
                if (enemyActionRange.isDetectPlayer)
                {
                    SetAIState(ENEMYAISTS.ATTACKPLAYER, 10.0f);
                }
                else
                {
                    SetAIState(ENEMYAISTS.LOITER, 5.0f);
                }
                enemyCtrl.ActionMove(0.0f);
                break;

            case ENEMYAISTS.LOITER:
                if (enemyActionRange.isDetectPlayer)
                {
                    enemyCtrl.ActionMove(0.0f);
                    SetAIState(ENEMYAISTS.ATTACKPLAYER, 10.0f);
                }
                else
                {
                    if (!isRotate)
                    {
                        isRotate = true;
                        SetTarget();
                        enemyCtrl.ActionLookUp(targetPoint);
                    }
                    if (!enemyCtrl.tryLookUp)
                    {
                        if (Physics.Raycast(rayStart.position, transform.forward, 2.0f) ||
                                !enemyCtrl.ActionMoveToNear(enemyCtrl.target, 1.0f, enemyCtrl.moveSpeed))
                        {
                            SetAIState(ENEMYAISTS.WAIT, Random.Range(1.0f, 3.0f));
                        }
                    }

                }
                break;

            case ENEMYAISTS.ATTACKPLAYER:
                if (!isRotate)
                {
                    isRotate = true;
                    enemyCtrl.ActionLookUp(player.transform.position);
                }
                if (!enemyCtrl.tryLookUp)
                {
                    
                }
                break;

            case ENEMYAISTS.WAIT:
                enemyCtrl.ActionMove(0.0f);
                break;
        }
    }

    public void Attack_A()
    {

    }

    public void Attack_B()
    {

    }

    public void SetTarget()
    {
        Vector3 target = Vector3.zero;
        target.x = Random.Range(minX, maxX);
        target.z = Random.Range(minZ, maxZ);
        targetPoint = target;
    }
}
