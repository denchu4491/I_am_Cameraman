using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionRange_d : MonoBehaviour {

    public Transform lookTarget;
    [System.NonSerialized] public bool isDetectPlayer = false;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            isDetectPlayer = true;
            lookTarget = col.transform;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            isDetectPlayer = false;
            lookTarget = null;
        }
    }
}
