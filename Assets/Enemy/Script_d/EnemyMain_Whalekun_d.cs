using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain_Whalekun_d : EnemyMain_d {

    bool isRotate;

    public override void FixedUpdateAI()
    {
        //Debug.Log(string.Format(">>> aiState {0}",aiState));
        switch (aiState)
        {
            case ENEMYAISTS.ACTIONSELECT:
                if (enemyCtrl.enemyActionRange.isDetectPlayer)
                {
                    SetAIState(ENEMYAISTS.ATTACKPLAYER, -1.0f);
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
                if (enemyCtrl.enemyActionRange.isDetectPlayer)
                {
                    SetAIState(ENEMYAISTS.ATTACKPLAYER, -1.0f);
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
}
