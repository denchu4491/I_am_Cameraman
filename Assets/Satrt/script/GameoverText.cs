using UnityEngine;
using System.Collections;
public class GameoverText : MonoBehaviour {


    //static public bool flag=false;
    static public Animator ani;

    void Start() {
        ani = GetComponent<Animator>();
    }

    static public void action() {
        ani.SetTrigger("disappear");
    }

}
