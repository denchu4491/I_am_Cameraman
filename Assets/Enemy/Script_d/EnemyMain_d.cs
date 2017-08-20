using System.Collections;
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
    [System.NonSerialized] public EnemyActionRange_d enemyActionRange;
    protected EnemyController_d enemyCtrl;
    protected GameObject player;
    protected Transform rayStart;
    protected float aiActionTimeLength = 0.0f;
    protected float aiActionTimeStart = 0.0f;

    public virtual void Awake()
    {
        enemyCtrl = GetComponent<EnemyController_d>();
        enemyActionRange = GetComponentInChildren<EnemyActionRange_d>();
        player = GameObject.Find("Player");
        rayStart = transform.Find("RayStart");
    }

	// Use this for initialization
	public virtual void Start () {

	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    public virtual void FixedUpdate()
    {
        if (BeginEnemyCommonWork())
        {
            FixedUpdateAI();
            if (aiActionTimeLength > 0) {
                EndEnemyCommonWork();
            }
        }
    }

    public virtual void FixedUpdateAI()
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

    public bool RayCheck(Vector3 pos, float distance)
    {
        int cnt = 0;
        RaycastHit[] hit = new RaycastHit[3];

        for (int i = 0; i < hit.Length; i++)
        {
            Vector3 direction = new Vector3(player.transform.position.x,
                player.transform.position.y + 0.3f + (0.2f * i), player.transform.position.z) - pos;
            if(Physics.Raycast(pos, direction.normalized, out hit[i], distance) && hit[i].collider.tag == "Player")
            {
                cnt++;
            }
            //Debug.Log(i + " " + hit[i].collider.tag);
        }
        if(cnt >= 2)
        {
            return true;
        }
        return false;
    }
}
