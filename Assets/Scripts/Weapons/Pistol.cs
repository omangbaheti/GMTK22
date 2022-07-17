using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : LongRangeWeapon
{
    protected override void Shoot()
    {
        if(!canShoot) return;
        gameObject.GetComponent<PlayOnSpawn>().PlaySound();
        GameObject bullet = Instantiate(bulletProjectile,bulletSpawner.position, bulletSpawner.rotation);
        canShoot = false;
        StartCoroutine(nameof(Cooldown));
    }
    
}
