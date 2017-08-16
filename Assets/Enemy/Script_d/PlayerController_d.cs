using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_d : MonoBehaviour {
    public float speed;
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public Vector3 localGravity;
    Rigidbody rigidbodyP;
    private bool activeSts;
    
    void Awake()
    {
        rigidbodyP =  GetComponent<Rigidbody>();
        rigidbodyP.useGravity = false;
        activeSts = true;
    }

    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f));
        }
        if(Input.GetKey(KeyCode.UpArrow) && transform.rotation.x >= -70)
        {
            //transform.Rotate(new Vector3(-1.0f, 0.0f, 0.0f));
        }
        if(Input.GetKey(KeyCode.DownArrow) && transform.rotation.x <= 70)
        {
            //transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f));
        }
        */

        /*
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(0, h, 0);
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("fire");
        }
    }

    void FixedUpdate()
    {
        if (!activeSts)
            return;

        SetLocalGravity();

        float h = 0;
        if (Input.GetKey(KeyCode.RightArrow))
            h += 1.0f;
        if (Input.GetKey(KeyCode.LeftArrow))
            h -= 1.0f;
        Direction(h * horizontalSpeed);

        float x = 0.0f, z = 0.0f;
        if (Input.GetKey(KeyCode.W))
            z += 1.0f;
        if (Input.GetKey(KeyCode.S))
            z -= 1.0f;
        if (Input.GetKey(KeyCode.D))
            x += 1.0f;
        if (Input.GetKey(KeyCode.A))
            x -= 1.0f;
        Move(x, z);
    }

    void SetLocalGravity()
    {
        rigidbodyP.AddForce(localGravity, ForceMode.Acceleration);
    }

    void Direction(float _h)
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0.0f, _h, 0.0f));
        rigidbodyP.MoveRotation(rigidbodyP.rotation * deltaRotation);
    }

    void Move(float _x,float _z)
    {
        Vector3 pos = (_z * transform.forward + _x * transform.right) * speed;
        rigidbodyP.velocity = new Vector3(pos.x, rigidbodyP.velocity.y, pos.z);
    }
}
