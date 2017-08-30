using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenRock1 : MonoBehaviour {

    public GameObject bigrock;   //壊れたときの大きい岩
    public GameObject smallrocks; // 壊れて飛び散るいわ
    public GameObject smoke; //破壊時の煙
    public float power; //上向きの爆発力
    
   
    
    Rigidbody rb2;
    Transform children2;
    Collider collider1;
    bool flag = false, hit = false;


   //初期化と値の取得
    void Start() {

        bigrock.SetActive(true);
        smallrocks.SetActive(false);
        collider1 = GetComponent<Collider>();
      //  auodio.clip = souns;
        
    }

    void Update() {
        //null参照を防ぐ
        if (smallrocks != null) {
            //子のtransform取得
            if (smallrocks.activeInHierarchy == true && flag == false) {
                flag = true;
                children2 = smallrocks.GetComponentInChildren<Transform>();
            }
            //飛ばした岩をだんだん小さくする
            if (smallrocks.activeInHierarchy == true && flag == true) {
                foreach (Transform child in children2) {
                    if (child.transform.localScale.x > 0 && child.transform.localScale.y > 0 && child.transform.localScale.z > 0) {
                        child.transform.localScale += new Vector3(-0.002f, -0.002f, -0.002f);
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if(col.isTrigger || col.tag == "Enemy" || col.tag == "EnemyBody" || col.tag == "EnemyArm"  || col.tag == "Breakable"|| hit)
        {
            return;
        }

        hit = true;
        collider1.enabled = false;

        //煙生成、破壊
        GameObject SM;
        SM = Instantiate(smoke,gameObject.transform);
        Destroy(SM, 4f);
       
        //飛んできた岩を殺して、小さい岩をアクティブにして飛ばす。念のため親も破壊
        Destroy(bigrock);
        Destroy(gameObject, 6f);
        smallrocks.SetActive(true);
        rb2 = smallrocks.GetComponent<Rigidbody>();
        rb2.AddForce(UnityEngine.Random.onUnitSphere * power);
        rb2.AddForce(0, 100, 0);
        Destroy(smallrocks, 5f);

    }

}
