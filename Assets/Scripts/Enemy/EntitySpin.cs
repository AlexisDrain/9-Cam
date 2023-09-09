using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpin : MonoBehaviour
{
    public Vector3 initialRotation = new Vector3(0f, 0f, 0f);

    public Vector2 spinSpeedRange = new Vector2(3f, 15f);
    public bool randomReverse = true;

    public Vector3 rotationAngle = new Vector3(0f, 1f, 0f);
    private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();

        GameManager.playerRevive.AddListener(ResetEntity);
        ResetEntity();

    }
    // FixedUpdate: KINEMATIC ONLY
    public void FixedUpdate() {
        if (myRigidbody.isKinematic) {
            float speed = 0f;
            if (randomReverse) {
                speed = (Random.Range(0, 2) * 2 - 1) * Random.Range(spinSpeedRange.x, spinSpeedRange.y);
            } else {
                speed = Random.Range(spinSpeedRange.x, spinSpeedRange.y);
            }

            Quaternion deltaRotation = Quaternion.Euler(rotationAngle * speed * Time.fixedDeltaTime);
            myRigidbody.MoveRotation(myRigidbody.rotation * deltaRotation);
        }
    }
    public void ResetEntity() {
        transform.rotation = Quaternion.Euler(initialRotation);
        myRigidbody.rotation = Quaternion.Euler(initialRotation);

        if (myRigidbody.isKinematic == false) {
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.angularVelocity = Vector3.zero;
        }

        float speed = 0f;
        if (randomReverse) {
            speed = (Random.Range(0, 2) * 2 - 1) * Random.Range(spinSpeedRange.x, spinSpeedRange.y);
        } else {
            speed = Random.Range(spinSpeedRange.x, spinSpeedRange.y);
        }
        if (myRigidbody.isKinematic == false) {
            myRigidbody.AddTorque(speed * rotationAngle);
        }

    }
}
