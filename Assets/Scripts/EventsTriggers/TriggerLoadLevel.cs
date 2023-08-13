using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLoadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartNewLevel(int level)
    {
        GameManager.SetNewLevel(level);
    }

}
