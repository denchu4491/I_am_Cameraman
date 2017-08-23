using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyCollider : MonoBehaviour {

    [System.NonSerialized] public bool isActionRangeEnter = true;

	void OnTriggerEnter(Collider col)
    {
        if(col.tag == "EnemyActionRange")
        {
            isActionRangeEnter = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "EnemyActionRange")
        {
            isActionRangeEnter = false;
        }
    }
}
