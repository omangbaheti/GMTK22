using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 500f;
    [SerializeField] protected float bulletLife = 0.3f;
    [SerializeField] public float damage = 20f;
    [SerializeField] public string target;

    public virtual void OnTriggerEnter(Collider body)
    {
        if (body.CompareTag(target))
        {
            body.GetComponent<IHealth>().AffectHealth(damage);
            Destroy(gameObject);
        }
    }
}
