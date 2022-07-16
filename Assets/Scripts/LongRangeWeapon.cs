using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LongRangeWeapon : MonoBehaviour
{
    [SerializeField] protected Transform bulletSpawner;
    [SerializeField] protected GameObject bulletProjectile;
    [SerializeField] protected float cooldownTime = 0.01f;
    
    protected Transform playerTransform;
    protected bool canShoot = true;

    private void Start()
    {
        InputManager.Shoot += Shoot;
        InputManager.ShootingVector += InterpolateWeapon;
    }
    
    protected void InterpolateWeapon(Vector3 shootingDirection)
    {
        transform.forward = shootingDirection;
    }

    protected virtual void  Shoot()
    {
        Debug.Log("ShootCalled");
        if(!canShoot) return;
        canShoot = false;
        
        StartCoroutine(nameof(Cooldown));
    }

    protected IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
    
    public void OnDisable()
    {
        InputManager.Shoot -= Shoot;
        InputManager.ShootingVector -= InterpolateWeapon;
    }
    
}
