using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEntity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DisableEntityFunc()
    {
        gameObject.SetActive(false);
    }
}
