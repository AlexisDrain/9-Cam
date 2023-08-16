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

    private bool isCrouching = false;
    public float standingHeight = 4f;
    public float crouchingHeight = 2f;

    public GameObject graphicGirl;
    private Rigidbody myRigidbody;
    private CapsuleCollider myCapsuleCollider;
    private Animator girlAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myCapsuleCollider = GetComponent<CapsuleCollider>();
        girlAnimator = graphicGirl.GetComponent<Animator>();
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

        // crouch
        if (Input.GetButtonDown("Crouch")) {
            isCrouching = true;
            girlAnimator.SetTrigger("Crouch");
            print("sit");
        }
        if (Input.GetButtonUp("Crouch")) {
            isCrouching = false;
            girlAnimator.SetTrigger("Stand");
            print("stand");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.playerIsAlive == false) {
            return;
        }

        // crouch
        if (isCrouching) {
            if (myCapsuleCollider.height > crouchingHeight + 0.1f) {
                myCapsuleCollider.height = Mathf.Lerp(myCapsuleCollider.height, crouchingHeight, 0.1f);
            }
        } else {
            if (myCapsuleCollider.height < standingHeight + 0.1f) {
                myCapsuleCollider.height = Mathf.Lerp(myCapsuleCollider.height, standingHeight, 0.1f);
            }
        }

        // regular movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0) {
            transform.eulerAngles += new Vector3(0, h * roationSpeed, 0);
            girlAnimator.SetBool("IsWalking", true);
        } else {
            girlAnimator.SetBool("IsWalking", false);
        }
        if (v != 0) {
            myRigidbody.AddForce(v * transform.forward * forwardSpeed, ForceMode.Impulse);

            girlAnimator.SetBool("IsWalking", true);
            //myRigidbody.velocity = v * transform.forward * forwardSpeed;
            //myRigidbody.AddForce(v * transform.forward * forwardSpeed);
        } else {
            myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            girlAnimator.SetBool("IsWalking", false);
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
