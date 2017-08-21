using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain_Whalekun_d : EnemyMain_d {

    public bool canAction, canLoiter, canAttack;                // 行動、徘徊、攻撃の許可
    private bool isRotate = false, isTarget = false;
    public int loop, index;                                     // 徘徊用変数
    public Transform[] wayLoop0, wayLoop1;
    private Transform[][] wayPointList = new Transform[2][];    
    public bool goloop1;             // ループの強制変更
    private int direction = 0;       // 0,1で徘徊の方向を決める

    public override void Awake()
    {
        base.Awake();

        wayPointList[0] = wayLoop0;
        wayPointList[1] = wayLoop1;
    }

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
                            //Debug.Log("LOITER " + loop + " " + index);
                            isRotate = true;
                            enemyCtrl.ActionLookUp(wayPointList[loop][index].position);
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
        if(index == 0 && direction == 1)
        {
            direction = 0;
            if (goloop1) loop = 1;
            else
            {
                int next = Random.Range(0, 2);
                if(loop != next)
                {
                    loop = next;
                    index = -1;
                }
            }
        }

        switch (loop) {
            case 0:
                if (++index == wayPointList[loop].Length) index = 0;
                if (index != 0 && direction == 0) direction = 1;
                break;

            case 1:
                if (direction == 0)
                {
                    if (++index == wayPointList[loop].Length - 1) direction = 1;
                }
                else if (direction == 1) index--;
                break;
        }
    }

    void SearchIndex()
    {
        int near = -1, nearLoop = -1;
        float sqrdistance = 0.0f;

        for (int i = 0; i < wayPointList.Length; i++)
        {
            for (int j = 0; j < wayPointList[i].Length; j++)
            {
                if (!Physics.Linecast(new Vector3(transform.position.x, transform.position.y + 10.0f, transform.position.z),
                    new Vector3(wayPointList[i][j].position.x, wayPointList[i][j].position.y + 10.0f, wayPointList[i][j].position.z)))
                {
                    Vector3 heading = wayPointList[i][j].position - transform.position;
                    if (near == -1 || heading.sqrMagnitude < sqrdistance)
                    {
                        near = j;
                        nearLoop = i;
                        sqrdistance = heading.sqrMagnitude;
                    }
                    // sqrdistanceは距離の積で、約30以下の距離ならばそのIndexに決定する(高速化のつもり)
                    if (sqrdistance < 1000)
                    {
                        index = j;
                        loop = i;
                        break;
                    }
                }
                if (near != -1 && i == wayPointList.Length - 1)
                {
                    index = near;
                    loop = nearLoop;
                }
            }
        }

        if (loop == 1) direction = Random.Range(0, 2);
        else direction = 1;
        if (near == -1) Debug.Log("WayPointLost");
    }
}
