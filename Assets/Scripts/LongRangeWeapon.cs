using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeWeapon : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawner;
    [SerializeField] private GameObject bulletProjectile;
    [SerializeField] private float cooldownTime = 0.01f;
    
    private Camera mainCamera;
    private Transform playerTransform;
    private bool canShoot = true;

    private void Start()
    {
        mainCamera = Camera.main;
        InputManager.Shoot += Shoot;
        InputManager.ShootingVector += InterpolateWeapon;
    }

    private void Update()
    {

    }

    private void InterpolateWeapon(Vector3 shootingDirection)
    {
        transform.forward = shootingDirection;
    }

    private void Shoot()
    {
        if(!canShoot) return;
        canShoot = false;
        GameObject bullet = Instantiate(bulletProjectile,bulletSpawner.position, bulletSpawner.rotation);
        Rigidbody bulletRigidBody = GetComponent<Rigidbody>();
        //bulletRigidBody.velocity = shootDirection.normalized * bulletSpeed;
        StartCoroutine(nameof(Cooldown));
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
    
    private void OnDisable()
    {
        InputManager.Shoot -= Shoot;
        InputManager.ShootingVector -= InterpolateWeapon;
    }
    
}
