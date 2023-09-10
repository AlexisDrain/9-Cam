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

    public float standingHeight = 4f;
    public float crouchingHeight = 2f;

    public GameObject graphicGirl;
    public GameObject shadow;

    [Header("ReadOnly")]
    public bool _isCrouching = false;
    public bool _onGround = false;
    public int _isRotating = 0;
    private float canJumpAgain = 0f;

    [Header("SFX")]
    public AudioSource audioSourceFootsteps;
    public AudioClip landSFX;
    public Vector2 landSFXPitch = new Vector2(0.8f, 1.2f);
    public AudioClip jumpSFX;
    public Vector2 jumpSFXPitch = new Vector2(0.8f, 1.2f);

    public AudioClip deathByFall;

    private Rigidbody myRigidbody;
    private CapsuleCollider myCapsuleCollider;
    //private Animator girlAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myCapsuleCollider = GetComponent<CapsuleCollider>();
        //girlAnimator = graphicGirl.GetComponent<Animator>();
    }
    private void Update() {
        if (GameManager.playerIsAlive == false) {
            return;
        }

        if (Input.GetButton("Jump") && _onGround == true && canJumpAgain <= 0f) {
            canJumpAgain = 0.5f;
            myRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            // sfx
            float randPitch = Random.Range(jumpSFXPitch.x, jumpSFXPitch.y);
            audioSourceFootsteps.pitch = randPitch;
            audioSourceFootsteps.clip = jumpSFX;
            audioSourceFootsteps.PlayWebGL();
        }

        if (Input.GetButtonUp("Jump") && myRigidbody.velocity.y > 0.1f) {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.5f, myRigidbody.velocity.z);
        }

        // crouch
        if (Input.GetButtonDown("Crouch")) {
            _isCrouching = true;
            //girlAnimator.SetTrigger("Crouch");
        }
        if (Input.GetButtonUp("Crouch")) {
            _isCrouching = false;
            //girlAnimator.SetTrigger("Stand");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(canJumpAgain > 0f) {
            canJumpAgain -= Time.deltaTime;
        }
        if (GameManager.playerIsAlive == false) {
            myRigidbody.velocity = new Vector3(0f, 0f, 0f);
            return;
        }
        RaycastHit hit;
        Physics.Linecast(transform.position, transform.position + new Vector3(0f, -1.5f, 0f), out hit, (1 << GameManager.worldMask) + (1 << GameManager.entityMask));
        if(hit.collider) {
            if (_onGround == false) {
                _onGround = true;

                // sfx
                float randPitch = Random.Range(landSFXPitch.x, landSFXPitch.y);
                audioSourceFootsteps.pitch = randPitch;
                audioSourceFootsteps.clip = landSFX;
                audioSourceFootsteps.PlayWebGL();
            }
        } else {
            _onGround = false;
        }

        // crouch
        if (_isCrouching) {
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
            _isRotating = (int)h;
        } else {
            _isRotating = 0;
        }
        if (v != 0) {
            myRigidbody.AddForce(v * transform.forward * forwardSpeed, ForceMode.Impulse);
        } else {
            myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
        }
        // max speed
        Vector3 velocityClamped = Vector3.ClampMagnitude(myRigidbody.velocity, maxVelocity);
        myRigidbody.velocity = new Vector3(velocityClamped.x, myRigidbody.velocity.y, velocityClamped.z);
        
        if(transform.position.y < -15f) {
            GameManager.SpawnLoudAudio(deathByFall, Vector2.one, 0.4f);
            GameManager.KillPlayer();
        }
    }
}
