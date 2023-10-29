using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class TriggerUnlockMedal : MonoBehaviour
{
    public void UnlockMedal(string medalName)
    {
        GameManager.ngHelper.UnlockMedal(medalName);
    }
}
