using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDressResizeHack : MonoBehaviour
{

    private Cloth componentCloth;

    void Start()
    {
        componentCloth = transform.Find("Dress").GetComponent<Cloth>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.localScale.magnitude < 0.8f && componentCloth.enabled == true) {
            componentCloth.enabled = false;
        } else if (transform.localScale.magnitude >= 0.8f && componentCloth.enabled == false) {
            componentCloth.enabled = true;
        }
    }
}
