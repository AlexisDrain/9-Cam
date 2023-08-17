using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibsSpawn : MonoBehaviour
{
    public Vector2 spawnForceImpulse = new Vector2(5f, 25f);

    private Vector3 spawnPos;

    private void Start() {
        spawnPos = transform.localPosition;
    }

    void OnEnable() {
        transform.localPosition = spawnPos;

        GetComponent<ParticleSystem>().Play();
        Vector3 randomTrajectory = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        GetComponent<Rigidbody>().AddForce(randomTrajectory * Random.Range(spawnForceImpulse.x, spawnForceImpulse.y), ForceMode.Impulse);
    }
}
