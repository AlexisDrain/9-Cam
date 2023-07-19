using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float roationSpeed = 1.8f;
    public float forwardSpeed = 7f;
    public float maxVelocity = 7f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0) {
            transform.eulerAngles += new Vector3(0, h * roationSpeed, 0);
        }
        if (v != 0) {
            GetComponent<Rigidbody>().AddForce(v * transform.forward * forwardSpeed, ForceMode.Impulse);
            GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, maxVelocity);
            //GetComponent<Rigidbody>().velocity = v * transform.forward * forwardSpeed;
            //GetComponent<Rigidbody>().AddForce(v * transform.forward * forwardSpeed);
        } else {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
