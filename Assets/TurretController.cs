using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public bool targetIsPlayer = true;
    public Transform bulletStart;
    public float bulletForce = 15f;
    public float defaultActiveCooldown = 3.0f;
    public float defaultShotCooldown = 1.0f;
    public AudioClip bulletShootSFX;

    private Transform target;
    private float shotCooldown = 0f;
    private bool isActive = false;
    
    void Start()
    {
        if (targetIsPlayer) {
            target = GameManager.player.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (shotCooldown > 0.0f) {
            shotCooldown -= Time.deltaTime;
            return;
        }

        if (isActive==false) {
            RaycastHit hit;
            Physics.Linecast(transform.position, target.transform.position, out hit, (1 << GameManager.worldMask) + (1 << GameManager.entityMask));
            if (hit.collider.CompareTag("Player")) {
                isActive = true;
                shotCooldown = defaultActiveCooldown;
                return; // restart this function so that we don't shoot immediately
            }
        }

        if (isActive == false || GameManager.playerIsAlive == false) {
            shotCooldown = defaultActiveCooldown;
            isActive = false;
            return;
        }
        
        transform.LookAt(target);

        shotCooldown = defaultShotCooldown;
        GameObject bullet = GameManager.pool_Bullets.Spawn(bulletStart.position);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.GetComponent<Rigidbody>().position = bulletStart.position;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);

        GetComponent<AudioSource>().clip = bulletShootSFX;
        GetComponent<AudioSource>().PlayWebGL();
    }
}
