using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    public PlayerController playerController;
    public Rigidbody girlRigidbody;
    public Animator girlAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 horizontalVelocity = new Vector2(girlRigidbody.velocity.x, girlRigidbody.velocity.z);

        // crouching takes precidence. It can happen on-ground and mid-air. Moving or not
        if (playerController._isCrouching) {
            girlAnimator.SetBool("IsCrouching", true);
            girlAnimator.SetBool("IsIdle", false);
            girlAnimator.SetBool("IsFlyingUp", false);
            girlAnimator.SetBool("IsFallingDown", false);
            girlAnimator.SetBool("IsRotatingLeft", false);
            girlAnimator.SetBool("IsRotatingRight", false);

            if (horizontalVelocity.magnitude > 0.5f || playerController._isRotating != 0) { // moving forward or rotating cause the crouchWalk animation
                girlAnimator.SetBool("IsWalking", true);
            } else {
                girlAnimator.SetBool("IsWalking", false);
            }
            return;
        } else {
            girlAnimator.SetBool("IsCrouching", false);
        }

        if (playerController._onGround == false) {
            if(girlRigidbody.velocity.y > 0.2f) {

                girlAnimator.SetBool("IsFlyingUp", true);
                girlAnimator.SetBool("IsIdle", false);

                return;
            }
            else {
                girlAnimator.SetBool("IsFlyingUp", false);
            }

            if (girlRigidbody.velocity.y < -0.2f) {
                girlAnimator.SetBool("IsFallingDown", true);
                girlAnimator.SetBool("IsIdle", false);

                return;
            } else {
                girlAnimator.SetBool("IsFallingDown", false);
            }
        } else {
            girlAnimator.SetBool("IsFlyingUp", false);
            girlAnimator.SetBool("IsFallingDown", false);
        }

        // moving forward taxes precidence over rotating
        if (horizontalVelocity.magnitude > 0.5f) {
            girlAnimator.SetBool("IsWalking", true);
            girlAnimator.SetBool("IsIdle", false);
            girlAnimator.SetBool("IsRotatingLeft", false);
            girlAnimator.SetBool("IsRotatingRight", false);
            return;
        } else {
            girlAnimator.SetBool("IsWalking", false);
        }


        if (playerController._isRotating != 0) {
            if(playerController._isRotating == -1) {
                girlAnimator.SetBool("IsRotatingLeft", true);
                girlAnimator.SetBool("IsRotatingRight", false);
            } else if (playerController._isRotating == 1) {
                girlAnimator.SetBool("IsRotatingLeft", false);
                girlAnimator.SetBool("IsRotatingRight", true);
            }
            girlAnimator.SetBool("IsIdle", false);
            return;
        } else {
            girlAnimator.SetBool("IsRotatingLeft", false);
            girlAnimator.SetBool("IsRotatingRight", false);
        }



        girlAnimator.SetBool("IsIdle", true);
    }
}
