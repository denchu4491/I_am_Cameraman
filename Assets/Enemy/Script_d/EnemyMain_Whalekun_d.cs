using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain_Whalekun_d : EnemyMain_d {

    public bool canAction, canLoiter, canAttack;    // アクションさせるか、徘徊させるか、攻撃させるか
    public Transform[] wayPointList;
    bool isRotate = false, isTarget = false;
    int index = 0;

    public override void FixedUpdateAI()
    {
        //Debug.Log(string.Format(">>> aiState {0}",aiState));
        if (canAction)
        {
            switch (aiState)
            {
                case ENEMYAISTS.ACTIONSELECT:
                    if (enemyActionRange.isDetectPlayer && canAttack)
                    {
                        if (RayCheck(rayStart.position, 30.0f))
                        {
                            SetAIState(ENEMYAISTS.ATTACKPLAYER, 10.0f);
                        }
                    }
                    else
                    {
                        if (isTarget)
                        {
                            isTarget = false;
                            SearchIndex();
                        }
                        if (canLoiter)
                        {
                            SetAIState(ENEMYAISTS.LOITER, 10.0f);
                        }
                    }
                    enemyCtrl.ActionMove(0.0f);
                    break;

                case ENEMYAISTS.WAIT:
                    enemyCtrl.ActionMove(0.0f);
                    break;

                case ENEMYAISTS.LOITER:
                    if (enemyActionRange.isDetectPlayer && canAttack)
                    {
                        if (RayCheck(rayStart.position, 30.0f))
                        {
                            isRotate = false;
                            enemyCtrl.ActionMove(0.0f);
                            SetAIState(ENEMYAISTS.ATTACKPLAYER, 10.0f);
                        }
                    }
                    else
                    {
                        if (!isRotate)
                        {
                            //Debug.Log("LOITER " + index);
                            isRotate = true;
                            enemyCtrl.ActionLookUp(wayPointList[index].position);
                        }
                        if (!enemyCtrl.tryLookUp)
                        {
                            if (!enemyCtrl.ActionMoveToNear(enemyCtrl.target, 3.0f))
                            {
                                isRotate = false;
                                NextIndex();
                                SetAIState(ENEMYAISTS.WAIT, Random.Range(1.0f, 3.0f));
                            }
                        }
                    }
                    break;

                case ENEMYAISTS.ATTACKPLAYER:
                    if (!isRotate)
                    {
                        isTarget = true;
                        isRotate = true;
                        enemyCtrl.ActionLookUp(player.transform.position);
                    }
                    if (!enemyCtrl.tryLookUp)
                    {
                        if (!enemyCtrl.ActionMoveToNear(enemyCtrl.target, 4.0f))
                        {
                            Attack_A();
                        }
                    }
                    break;
            }
        }
    }

    void Attack_A()
    {
        isRotate = false;
        enemyCtrl.ActionMove(0.0f);
        enemyCtrl.ActionAttack("Attack_Bite",1.0f);
        SetAIState(ENEMYAISTS.WAIT, 3.0f);
    }

    void NextIndex()
    {
        if (++index == wayPointList.Length) index = 0;
    }

    void SearchIndex()
    {
        int near = -1;
        float sqrdistance = 0.0f;

        for(int i = 0; i < wayPointList.Length; i++)
        {
            if(!Physics.Linecast(new Vector3(transform.position.x, transform.position.y + 10.0f, transform.position.z),
                new Vector3(wayPointList[i].position.x, wayPointList[i].position.y + 10.0f, wayPointList[i].position.z) ))
            {
                Vector3 heading = wayPointList[i].position - transform.position;
                if(near == -1 || heading.sqrMagnitude < sqrdistance)
                {
                    near = i;
                    sqrdistance = heading.sqrMagnitude;
                }
                // sqrdistanceは距離の積で、約30以下の距離ならばそのIndexに決定する(高速化のつもり)
                if(sqrdistance < 1000)
                {
                    index = i;
                    break;
                }
            }
            if(near != -1 && i == wayPointList.Length - 1)
            {
                index = near;
            }
        }
        //if (near == -1) Debug.Log("WayPointLost");
    }
}
