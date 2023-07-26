using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpPower = 5f;
    public float roationSpeed = 1.8f;
    public float forwardSpeed = 7f;
    public float maxVelocity = 7f;

    private Rigidbody myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    private void Update() {
        if (GameManager.playerIsAlive == false) {
            return;
        }

        // very buggy. can make you double jump sometimes. use a line cast
        if (Input.GetButtonDown("Jump") && Mathf.Abs(myRigidbody.velocity.y) < 0.1f) {
            myRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        if (Input.GetButtonUp("Jump") && myRigidbody.velocity.y > 0.1f) {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.5f, myRigidbody.velocity.z);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.playerIsAlive == false) {
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0) {
            transform.eulerAngles += new Vector3(0, h * roationSpeed, 0);
        }
        if (v != 0) {
            myRigidbody.AddForce(v * transform.forward * forwardSpeed, ForceMode.Impulse);

            //myRigidbody.velocity = v * transform.forward * forwardSpeed;
            //myRigidbody.AddForce(v * transform.forward * forwardSpeed);
        } else {
            myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
        }
        // max speed
        Vector3 velocityClamped = Vector3.ClampMagnitude(myRigidbody.velocity, maxVelocity);
        myRigidbody.velocity = new Vector3(velocityClamped.x, myRigidbody.velocity.y, velocityClamped.z);
        
        if(transform.position.y < -15f) {
            print("Player is falling");
            GameManager.KillPlayer();
        }
    }
}
