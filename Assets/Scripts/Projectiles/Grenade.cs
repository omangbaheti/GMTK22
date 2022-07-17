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
        bulletRigidBody.AddForce(transform.forward * bulletSpeed + transform.up * upwardThrust, ForceMode.Impulse);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Debug.Log("destroyed");
            Destroy(gameObject);
        }
    }

}
