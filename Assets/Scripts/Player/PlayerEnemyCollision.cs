using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Alexis Clay Drain
 */

public class PlayerEnemyCollision : MonoBehaviour
{
	public int health = 3;
    public AudioClip hurtAudioClip;

    void OnTriggerEnter(Collider col)
	{

		if (GameManager.playerIsAlive == false) {
			return;
		}

        if (col.CompareTag("Enemy")) {
            GameManager.SpawnLoudAudio(hurtAudioClip);
            GameManager.KillPlayer();
			
		}
		if (col.CompareTag("Bullet")) {
            GameManager.SpawnLoudAudio(hurtAudioClip);
            health -= 1;
			if (health <= 0) {
                GameManager.KillPlayer();
            }
        }
    }

    void OnCollisionEnter(Collision col) {

        if (GameManager.playerIsAlive == false) {
            return;
        }

        if (col.collider.CompareTag("Enemy")) {
            GameManager.SpawnLoudAudio(hurtAudioClip);
            GameManager.KillPlayer();

        }
        if (col.collider.CompareTag("Bullet")) {
            GameManager.SpawnLoudAudio(hurtAudioClip);
            health -= 1;
            if (health <= 0) {
                GameManager.KillPlayer();
            }
        }
    }
}
