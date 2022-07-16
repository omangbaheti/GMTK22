using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody bulletRigidBody = GetComponent<Rigidbody>();
        bulletRigidBody.velocity += transform.forward * bulletSpeed;
        Destroy(gameObject, bulletLife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
