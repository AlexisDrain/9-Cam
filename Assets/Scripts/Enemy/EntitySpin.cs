using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpin : MonoBehaviour
{
    public float initialRotation = 0f;

    public Vector2 spinSpeedRange = new Vector2(3f, 15f);
    public bool randomReverse = true;

    private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();

        //GameManagerChasm.resetEnemyCollisions.AddListener(ResetEntity);
        ResetEntity();

    }

    public void ResetEntity() {
        transform.rotation = Quaternion.Euler(0f, 0f, initialRotation);
        myRigidbody.rotation = Quaternion.Euler(0f, 0f, initialRotation);

        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;

        float speed = 0f;
        if (randomReverse) {
            speed = (Random.Range(0, 2) * 2 - 1) * Random.Range(spinSpeedRange.x, spinSpeedRange.y);
        } else {
            speed = Random.Range(spinSpeedRange.x, spinSpeedRange.y);
        }

        myRigidbody.AddTorque(new Vector3(0f, 0f, speed));

    }
}
