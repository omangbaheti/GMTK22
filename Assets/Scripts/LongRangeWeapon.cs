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
    private Vector3 shootDirection;
    private Transform playerTransform;
    private bool canShoot = true;

    private void Start()
    {
        mainCamera = Camera.main;
        InputManager.Shoot += Shoot;
    }

    private void Update()
    {
        InterpolateWeapon();
    }

    private void InterpolateWeapon()
    {
        Ray mouseCoordinates = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseCoordinates, out RaycastHit hit))
        {
            shootDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
            transform.forward = shootDirection;
        }

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
    }
    
}
