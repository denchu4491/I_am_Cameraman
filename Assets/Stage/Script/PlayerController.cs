using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {


    public float speed = 10;
    public GameObject Maincamera;
    public GameObject TargetObject;

    private Vector3 lastMousePosition;
    private Vector3 newAngle = new Vector3(0,0,0);

    private void Start() {
        DOTween.Init(false,true,LogBehaviour.ErrorsOnly);
    }


    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            newAngle = Maincamera.transform.localEulerAngles;
            lastMousePosition = Input.mousePosition;
        }else if (Input.GetMouseButton(0)) {
            newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * 0.1f;
            newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * 0.1f;
            Maincamera.gameObject.transform.localEulerAngles = newAngle;

            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1)){

            var rotation = Quaternion.LookRotation(TargetObject.transform.position-Maincamera.transform.position);

            Maincamera.transform.DORotateQuaternion(rotation, 0.5f)
                .SetEase(Ease.InOutBounce)
                .OnComplete(() => Debug.Log("Finished"));

        }


    }

    void FixedUpdate() {


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(x * speed,0,z*speed);
        
    }
}
