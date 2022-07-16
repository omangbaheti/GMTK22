using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    [SerializeField] private float upwardThrust;

    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody bulletRigidBody = GetComponent<Rigidbody>();
        //bulletRigidBody.velocity += transform.forward * bulletSpeed + transform.up * upwardThrust;
        bulletRigidBody.AddForce(transform.forward * bulletSpeed + transform.up * upwardThrust, ForceMode.Impulse);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
            Destroy(gameObject);
    }

}
