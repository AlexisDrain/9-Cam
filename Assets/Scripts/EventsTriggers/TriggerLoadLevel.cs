using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class TriggerLoadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartNewLevel(int level)
    {

        GameManager.gameManagerObj.GetComponent<GameManager>().SetNewLevel(level);
    }
    public void IncrementAndStartLevel() {
        GameManager.currentLevelInt = GameManager.currentLevelInt + 1;
        int newLevel = GameManager.currentLevelInt;

        GameManager.gameManagerObj.GetComponent<GameManager>().SetNewLevel(newLevel);
    }

}
