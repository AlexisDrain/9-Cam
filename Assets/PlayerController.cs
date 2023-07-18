using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
            transform.eulerAngles += new Vector3(0, h * 1.8f, 0);
        }
        if (v != 0) {
            GetComponent<Rigidbody>().AddForce(v * transform.forward * 7f);
        }
    }
}
