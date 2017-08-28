using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain_Golem_B : EnemyMain_d {

    public EnemyActionRange_d enemyActionRange;
    public float attackMoveSpeed, attackWaitTime, nearAttackRange;
    private Vector3 firstPosition, rangeSize, rangeCenter, targetPoint;
    private float minX, maxX, minZ, maxZ;
    private bool isRotate, isNearTarget;
    public GameObject fireObject;

    public override void Awake()
    {
        base.Awake();

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
                isNearTarget = false;

                if (enemyActionRange.isDetectPlayer)
                {
                    SetAIState(ENEMYAISTS.ATTACKPLAYER, 10.0f);
                }
                else
                {
                    SetAIState(ENEMYAISTS.LOITER, 20.0f);
                }
                enemyCtrl.ActionMove(0.0f);
                break;

            case ENEMYAISTS.LOITER:
                if (enemyActionRange.isDetectPlayer)
                {
                    isRotate = false;
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
                                !enemyCtrl.ActionMoveToNear(enemyCtrl.target, 2.0f, 0.0f))
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
                    if (GetDistancePlayerNear(nearAttackRange) && GetDistanceYPlayerNear(5.0f))
                    {
                        isNearTarget = true;
                    }
                }
                if (!enemyCtrl.tryLookUp)
                {
                    if (isNearTarget)
                    {
                        if (!enemyCtrl.ActionMoveToNear(enemyCtrl.target, 5.0f, attackMoveSpeed))
                        {
                            Attack_A();
                        }
                    }
                    else
                    {
                        Attack_B();
                    }
                }
                break;

            case ENEMYAISTS.WAIT:
                enemyCtrl.ActionMove(0.0f);
                break;
        }
    }

    public void Attack_A()
    {
        enemyCtrl.ActionMove(0.0f);
        enemyCtrl.ActionAttack("Attack");
        SetAIState(ENEMYAISTS.WAIT, attackWaitTime);
    }

    public void Attack_B()
    {   
        Transform goFire = transform.Find("Muzzle");
        GameObject go = Instantiate(fireObject, goFire.position, goFire.rotation
                            /*(Quaternion.LookRotation(player.transform.position - goFire.position))*/ ) as GameObject;
        go.GetComponent<FireBullet>().ownwer = transform;
        SetAIState(ENEMYAISTS.WAIT, attackWaitTime);
    }

    public void SetTarget()
    {
        targetPoint.x = Random.Range(minX, maxX);
        targetPoint.z = Random.Range(minZ, maxZ);
    }
}
