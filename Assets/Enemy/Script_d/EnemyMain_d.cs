﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMYAISTS
{
    ACTIONSELECT,   //アクション選択
    WAIT,           //一定時間待つ
    LOITER,         //徘徊する
    ATTACKPLAYER,   //近づいて攻撃する
}

public class EnemyMain_d : MonoBehaviour {

    [System.NonSerialized] public ENEMYAISTS aiState = ENEMYAISTS.ACTIONSELECT;
    protected EnemyController_d enemyCtrl;
    protected GameObject player;

    protected float aiActionTimeLength = 0.0f;
    protected float aiActionTimeStart = 0.0f;

	// Use this for initialization
	public virtual void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    public virtual void FixedUpdate()
    {

    }

    public bool BeginEnemyCommonWork()
    {
        enemyCtrl.animator.enabled = true;

        if (!CheckAction())
        {
            return false;
        }
        return true;
    }

    public void EndEnemyCommonWork()
    {
        // アクションのリミット時間をチェック
        float time = Time.fixedTime - aiActionTimeStart;
        if(time > aiActionTimeLength)
        {
            aiState = ENEMYAISTS.ACTIONSELECT;
        }
    }

    public bool CheckAction()
    {
        // 状態チェック
        AnimatorStateInfo stateInfo = enemyCtrl.animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.tagHash == EnemyController_d.ANISTE_Attack)
        {
            return false;
        }
        return true;
    }

    public void SetAIState(ENEMYAISTS sts,float t)
    {
        aiState = sts;
        aiActionTimeStart = Time.fixedTime;
        aiActionTimeLength = t;
    }
}