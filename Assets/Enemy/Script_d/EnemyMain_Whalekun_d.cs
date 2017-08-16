using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain_Whalekun_d : EnemyMain_d {

    bool isRotate;
    public Transform[] wayPointList;
    int index = 0;

    public override void FixedUpdateAI()
    {
        //Debug.Log(string.Format(">>> aiState {0}",aiState));
        switch (aiState)
        {
            case ENEMYAISTS.ACTIONSELECT:
                if (enemyActionRange.isDetectPlayer)
                {
                    if (RayCheck(rayStart.position, 30.0f))
                    {
                        SetAIState(ENEMYAISTS.ATTACKPLAYER, 10.0f);
                    }
                }
                else
                {
                    SetAIState(ENEMYAISTS.LOITER, -1.0f);
                }
                enemyCtrl.ActionMove(0.0f);
                break;

            case ENEMYAISTS.WAIT:
                enemyCtrl.ActionMove(0.0f);
                break;

            case ENEMYAISTS.LOITER:
                if (enemyActionRange.isDetectPlayer)
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
                        isRotate = true;
                        Debug.Log(index);
                        enemyCtrl.ActionLookUp(wayPointList[index].position);
                    }
                    if (!enemyCtrl.tryLookUp)
                    {
                        if (!enemyCtrl.ActionMoveToNear(enemyCtrl.target, 3.0f))
                        {
                            //Debug.Log("hoge");
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
}
