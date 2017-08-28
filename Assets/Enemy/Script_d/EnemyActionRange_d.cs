using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionRange_d : MonoBehaviour {

    //public Transform lookTarget;
    [System.NonSerialized] public bool isDetectPlayer = false;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            isDetectPlayer = true;
            //lookTarget = col.transform;
        }
        if(col.tag == "PlayerSearch")
        {
            isDetectPlayer = true;
            Invoke("DetectOff", 1.0f);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            isDetectPlayer = false;
            //lookTarget = null;
        }
    }

    void DetectOff()
    {
        isDetectPlayer = false;
    }
}
