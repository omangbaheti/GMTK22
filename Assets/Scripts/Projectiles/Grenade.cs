using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : Projectile
{
    [SerializeField] private float upwardThrust;
    [SerializeField] private float impactRadius = 4f;


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
            Explode();
        }
    }

    private void Explode()
    {
        var objectsInsideArea = Physics.OverlapSphere(transform.position, impactRadius);
        foreach (Collider body in objectsInsideArea)
        {
            if (body.CompareTag(target))
            {
                Debug.Log(body);
                body.GetComponent<IHealth>().AffectHealth(damage);
            }
        }
        Destroy(gameObject);
    }
}
