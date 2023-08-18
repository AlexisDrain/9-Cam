using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTransform : MonoBehaviour
{
    public bool axisYOnly = true;
    public Transform target;
    public bool targetIsPlayer = true;
    // Start is called before the first frame update
    void Start()
    {
        if (targetIsPlayer) {
            target = GameManager.player.transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPostition;
        if (axisYOnly) {
            targetPostition = new Vector3(target.position.x,
                                       this.transform.position.y,
                                       target.position.z);
        } else {
            targetPostition = target.position;
        }
        
        this.transform.LookAt(targetPostition);
    }
}
