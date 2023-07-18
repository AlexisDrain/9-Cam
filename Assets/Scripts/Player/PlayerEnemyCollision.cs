using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Alexis Clay Drain
 */

public class PlayerEnemyCollision : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
	{

		if (GameManager.playerIsAlive == false) {
			return;
		}

        if (col.CompareTag("Enemy")) {
			GameManager.gameManagerObj.GetComponent<GameManager>().KillPlayer();
			
		}
	}
}
