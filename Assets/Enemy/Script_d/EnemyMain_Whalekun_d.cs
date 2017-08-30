using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain_Whalekun_d : EnemyMain_d {

    [System.NonSerialized] public EnemyActionRange_d enemyActionRange;
    public bool canAction, canLoiter, canAttack;                        // 行動、徘徊、攻撃の許可
    private bool isRotate = false, isTarget = false, isStray = false;   // 目標へ視点を向けたか、Playerを発見しているか、徘徊ループからはぐれているか
    public int loop, index;                                             // 初回の徘徊ポイント
    public Transform[] wayLoop0, wayLoop1;
    private Transform[][] wayPointList = new Transform[2][];    
    public bool goloop1;             // ループの強制変更
    private int direction = 0;       // 0,1で徘徊の方向を決める
    public float attackMoveSpeed, attackWaitTime = 3.0f;

    public override void Awake()
    {
        base.Awake();

        enemyActionRange = GetComponentInChildren<EnemyActionRange_d>();
        wayPointList[0] = wayLoop0;
        wayPointList[1] = wayLoop1;
    }

    public override void FixedUpdateAI()
    {
        //Debug.Log(string.Format(">>> aiState {0}",aiState));
        if (!canAction) return;
        switch (aiState)
        {
            case ENEMYAISTS.ACTIONSELECT:
                isRotate = false;
                if ((enemyActionRange.isDetectPlayer || enemyActionRange.isShutterPlayer) && canAttack)
                {
                    if (RayCheck(rayStart.position, 30.0f))
                    {
                        SetAIState(ENEMYAISTS.ATTACKPLAYER, 10.0f);
                    }
                }
                if(aiState != ENEMYAISTS.ATTACKPLAYER)
                {
                    if (isTarget)
                    {
                        isTarget = false;
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
                if ((enemyActionRange.isDetectPlayer || enemyActionRange.isShutterPlayer) && canAttack)
                {
                    if (RayCheck(rayStart.position, 30.0f))
                    {
                        isRotate = false;
                        enemyCtrl.ActionMove(0.0f);
                        SetAIState(ENEMYAISTS.ATTACKPLAYER, 10.0f);
                    }
                }
                if(aiState != ENEMYAISTS.ATTACKPLAYER)
                {
                    if (!isRotate)
                    {
                        isRotate = true;
                        if (isStray)
                        {
                            SearchIndex();
                        }
                        //Debug.Log("LOITER " + loop + " " + index);
                        enemyCtrl.ActionLookUp(wayPointList[loop][index].position);
                    }
                    if (!enemyCtrl.tryLookUp)
                    {
                        if (!enemyCtrl.ActionMoveToNear(enemyCtrl.target, 3.0f, 0.0f))
                        {
                            isStray = false;
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
                    isStray = true;
                    isTarget = true;
                    enemyCtrl.ActionLookUp(player.transform.position);
                }
                if (!enemyCtrl.tryLookUp)
                {
                    if (!enemyCtrl.ActionMoveToNear(enemyCtrl.target, 4.0f, attackMoveSpeed))
                    {
                        Attack_A();
                    }
                }
                break;
        }
    }

    void Attack_A()
    {
        enemyCtrl.ActionMove(0.0f);
        enemyCtrl.ActionAttack("Attack_Bite",1.0f);
        SetAIState(ENEMYAISTS.WAIT, attackWaitTime);
    }

    void NextIndex()
    {
        if(index == 0 && direction == 1)
        {
            int next;
            direction = 0;
            if (goloop1) next = 1;
            else
            {
                next = Random.Range(0, 2);
            }

            if (loop != next)
            {
                loop = next;
                index = -1;
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
                    if (index == wayPointList[loop].Length - 1) direction = 1;
                    else index++;
                }
                if (direction == 1) index--;
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
