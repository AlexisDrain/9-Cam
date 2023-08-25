using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public List<AudioClip> footsteps;
    public Vector2 footstepPitch;
    public float distanceToPlayFootstep = 1f;
    public AudioSource audioSourceFootsteps;

    private new Vector3 oldFootprintLocation;
    private new Vector3 currentFootprintLocation;
    private float currentDistanceTraveled = 0;
    void Start() {
        oldFootprintLocation = new Vector3(transform.position.x, 0f, transform.position.z);
        currentFootprintLocation = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentFootprintLocation = new Vector3(transform.position.x, 0f, transform.position.z);
        if (GameManager.player.GetComponent<PlayerController>()._onGround) {
            if (Vector3.Distance(currentFootprintLocation, oldFootprintLocation) > distanceToPlayFootstep) {
                oldFootprintLocation = currentFootprintLocation;
                PlayFootstep();
            }
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
