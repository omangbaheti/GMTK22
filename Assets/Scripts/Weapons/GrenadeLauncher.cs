using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : LongRangeWeapon
{
    // Start is called before the first frame update
    

    protected override void Shoot()
    {
        if(!canShoot) return;
        canShoot = false;
        
        GameObject bullet = Instantiate(bulletProjectile,bulletSpawner.position, bulletSpawner.rotation);
        GameObject muzzleFlash = Instantiate(base.muzzleFlash, bulletSpawner.position, bulletSpawner.rotation);
        Destroy(muzzleFlash, .1f);
        StartCoroutine(nameof(Cooldown));
    }
    
}
