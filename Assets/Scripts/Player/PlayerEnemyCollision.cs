using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Alexis Clay Drain
 */

public class PlayerEnemyCollision : MonoBehaviour
{
	public int health = 3;

    void OnTriggerEnter(Collider col)
	{

		if (GameManager.playerIsAlive == false) {
			return;
		}

        if (col.CompareTag("Enemy")) {
			GameManager.KillPlayer();
			
		}
		if (col.CompareTag("Bullet")) {
			print("Player took a bullet");
			health -= 1;
			if (health <= 0) {
                GameManager.KillPlayer();
            }
        }
    }
}
