﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain_Golem : EnemyMain_d{

    private EnemyBodyCollider enemyBodyCol;
    private EnemyActionRange_d enemyActionRange;
    public float attackMoveSpeed, attackWaitTime = 1.0f, attackRange = 40.0f;
    private Vector3 firstPosition, rangeSize, rangeCenter, targetPoint;
    private float minX, maxX, minZ, maxZ;
    private bool isRotate;

    public override void Awake()
    {
        base.Awake();

        enemyActionRange = GetComponentInParent<EnemyActionRange_d>();
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
                isRotate = false;
                if (enemyBodyCol.isActionRangeEnter && enemyActionRange.isDetectPlayer)
                {
                    if (RayCheck(rayStart.position, attackRange))
                    {
                        SetAIState(ENEMYAISTS.ATTACKPLAYER, 10.0f);
                    }
                }
                if(aiState != ENEMYAISTS.ATTACKPLAYER)
                {
                    SetAIState(ENEMYAISTS.LOITER, 10.0f);
                }
                enemyCtrl.ActionMove(0.0f);
                break;

            case ENEMYAISTS.LOITER:
                if (enemyBodyCol.isActionRangeEnter && enemyActionRange.isDetectPlayer)
                {
                    if (RayCheck(rayStart.position, attackRange))
                    {
                        enemyCtrl.ActionMove(0.0f);
                        SetAIState(ENEMYAISTS.ATTACKPLAYER, 10.0f);
                    }
                }
                if(aiState != ENEMYAISTS.ATTACKPLAYER)
                {
                    if (!isRotate)
                    {
                        isRotate = true;
                        if (!enemyBodyCol.isActionRangeEnter)
                        {
                            targetPoint = firstPosition;
                        }
                        else
                        {
                            SetTarget();
                        }
                        //Debug.Log(this.name + " " + targetPoint);
                        enemyCtrl.ActionLookUp(targetPoint);
                    }
                    if (!enemyCtrl.tryLookUp)
                    {
                        if (Physics.Raycast(rayStart.position, transform.forward, 2.0f) ||
                                !enemyCtrl.ActionMoveToNear(enemyCtrl.target, 1.0f, 0.0f))
                        {
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
                    SetAIState(ENEMYAISTS.WAIT, attackWaitTime);
                }
                break;

            case ENEMYAISTS.WAIT:
                enemyCtrl.ActionMove(0.0f);
                break;
        }
    }

    public void Attack()
    {
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
