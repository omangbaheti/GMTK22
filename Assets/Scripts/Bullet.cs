using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private float bulletLife = 5f;
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
