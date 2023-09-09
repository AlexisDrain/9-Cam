using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public List<AudioClip> footsteps;
    public Vector2 footstepPitch;
    public float distanceToPlayFootstep = 1f;
    public float rotation_TimeToPlayFootstepDefault = 1f;
    public AudioSource audioSourceFootsteps;

    private new Vector3 oldFootprintLocation;
    private new Vector3 currentFootprintLocation;

    private float rotation_TimeToPlayFootstepCurrent = 1f;

    private PlayerController playerController;
    void Start() {
        oldFootprintLocation = new Vector3(transform.position.x, 0f, transform.position.z);
        currentFootprintLocation = new Vector3(transform.position.x, 0f, transform.position.z);

        playerController = GameManager.player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // rotation footsteps
        if(playerController._isRotating != 0) {
            rotation_TimeToPlayFootstepCurrent -= Time.deltaTime;
            if(rotation_TimeToPlayFootstepCurrent <= 0f) {
                rotation_TimeToPlayFootstepCurrent = rotation_TimeToPlayFootstepDefault;
                PlayFootstep();
            }
        }

        // movement footsteps
        currentFootprintLocation = new Vector3(transform.position.x, 0f, transform.position.z);
        if (playerController._onGround) {
            if (Vector3.Distance(currentFootprintLocation, oldFootprintLocation) > distanceToPlayFootstep) {
                oldFootprintLocation = currentFootprintLocation;
                // distance also resets rotation footstep so that walking while moving doesn't result in more footsteps
                rotation_TimeToPlayFootstepCurrent = rotation_TimeToPlayFootstepDefault;
                PlayFootstep();
            }
        }
        Vector2 horizontalVelocity = new Vector2(GameManager.player.GetComponent<Rigidbody>().velocity.x, GameManager.player.GetComponent<Rigidbody>().velocity.z);
        if(horizontalVelocity.magnitude <= 0.5f) {
            oldFootprintLocation = currentFootprintLocation;
        }
    }

    public void PlayFootstep() {
        int stepSound = Random.Range(0, footsteps.Count);
        float randPitch = Random.Range(footstepPitch.x, footstepPitch.y);
        audioSourceFootsteps.clip = footsteps[stepSound];
        audioSourceFootsteps.pitch = randPitch;

        audioSourceFootsteps.PlayWebGL();
    }
}
