﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public bool targetIsPlayer = true;
    public bool canLookUp = true;
    public Transform overrideTarget;
    public Transform bulletStart;
    public float bulletForce = 15f;
    public int defaultSuccessiveShots = 99;
    public float defaultSuccessiveShotsCooldown = 3f;
    public float defaultActiveCooldown = 3.0f;
    public float defaultShotCooldown = 1.0f;
    public AudioClip bulletShootSFX;

    [Header("For use by other triggers")]
    public bool turretIsDisabled = false;
    private Transform target;
    private int currentSuccessiveShots = 0;
    private float shotCooldown = 0f;
    private float loseTargetCooldown = 0f;
    private bool isActive = false;
    
    void Start()
    {
        if (targetIsPlayer) {
            target = GameManager.player.transform;
        } else {
            target = overrideTarget;
        }

        ResetEntity();
        GameManager.playerRevive.AddListener(ResetEntity);
    }
    public void ResetEntity() {
        if (isActive) {
            TurretCamManager.EnableMiddleCamera();
        }
        shotCooldown = defaultActiveCooldown;
        turretIsDisabled = false;
        isActive = false;
        GetComponent<Renderer>().enabled = true;
    }
    public void BlackifyTurret() {
        turretIsDisabled = true;
        TurretCamManager.BlackifyMiddleCamera();
        GetComponent<Renderer>().enabled = false; // visual so that the turret doesn't appear on the black camera
    }

    // Update is called once per frame
    void FixedUpdate() {
        
        // demo victim
        if(overrideTarget != null) {
            //if (target.GetComponent<VictimHealth>().health <= 1) {
            //    shotCooldown = 2f;
            //    return;
            //}
        }
        // end demo victim
        
        if(turretIsDisabled) {
            return;
        }

        if(canLookUp == true) {
            transform.LookAt(target);
        } else if (canLookUp == false) {
            if (target.position.y < transform.position.y) {
                transform.LookAt(target);
            }
        }

        if (isActive == true) {
            RaycastHit hit;
            Physics.Linecast(transform.position, target.transform.position, out hit, (1 << GameManager.entityMask) + (1 << GameManager.worldMask));
            if (hit.collider && hit.collider.CompareTag("Player")) {
                // found target
                loseTargetCooldown = 2f;
            }

            if (loseTargetCooldown > 0f) {
                loseTargetCooldown -= Time.deltaTime;
            }
            if (loseTargetCooldown <= 0f) {
                isActive = false;
            }
        }

        if (shotCooldown > 0.0f) {
            shotCooldown -= Time.deltaTime;
            return;
        }

        if (isActive==false) {
            RaycastHit hit;
            Physics.Linecast(transform.position, target.transform.position, out hit, (1 << GameManager.entityMask) + (1 << GameManager.worldMask));
            if (hit.collider && hit.collider.CompareTag("Player")) {
                // found target
                isActive = true;
                shotCooldown = defaultActiveCooldown;
                loseTargetCooldown = 2f;
                return; // restart this function so that we don't shoot immediately
            }
        }

        if (isActive == false || GameManager.playerIsAlive == false) {
            shotCooldown = defaultActiveCooldown;
            isActive = false;
            return;
        }
        

        shotCooldown = defaultShotCooldown;
        currentSuccessiveShots += 1;
        if(currentSuccessiveShots > defaultSuccessiveShots) {
            shotCooldown = defaultSuccessiveShotsCooldown;
            currentSuccessiveShots = 0;
            isActive = false;
            return;
        }
        GameObject bullet = GameManager.pool_Bullets.Spawn(bulletStart.position);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.GetComponent<Rigidbody>().position = bulletStart.position;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);

        GetComponent<AudioSource>().clip = bulletShootSFX;
        GetComponent<AudioSource>().PlayWebGL();
    }
}
