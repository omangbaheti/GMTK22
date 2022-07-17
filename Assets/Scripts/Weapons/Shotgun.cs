using UnityEngine;

public class Shotgun : LongRangeWeapon
{
    [SerializeField] private int noOfBullets = 5;
    [SerializeField] private float spreadAngle = 20f;
   
    protected override void Shoot()
    {
        if(!canShoot) return;
        canShoot = false;
        for (int i = 0; i < noOfBullets; i++)
        {
            int spawnPosition = i - noOfBullets/2;
            Quaternion offset = Quaternion.Euler(0f, spawnPosition * spreadAngle, 0f);
            GameObject muzzleFlash = Instantiate(base.muzzleFlash, bulletSpawner.position, bulletSpawner.rotation);
            Destroy(muzzleFlash, .1f);

            GameObject bullet = Instantiate(bulletProjectile,bulletSpawner.position, bulletSpawner.rotation * offset);
        }
        StartCoroutine(nameof(Cooldown));
    }
}
