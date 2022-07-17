using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : LongRangeWeapon
{
    protected override void Shoot()
    {
        if(!canShoot) return;
        GameObject bullet = Instantiate(bulletProjectile,bulletSpawner.position, bulletSpawner.rotation);
        canShoot = false;
        StartCoroutine(nameof(Cooldown));
    }
    
}
