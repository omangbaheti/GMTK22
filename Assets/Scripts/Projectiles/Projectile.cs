using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 500f;
    [SerializeField] protected float bulletLife = 0.3f;
    [SerializeField] public float damage = 20f;
    [SerializeField] public string target;
    [SerializeField] private ParticleSystem explosion;
    
    private CinemachineVirtualCamera cinemachine;

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
        cinemachine = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin =
            cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public virtual void OnTriggerEnter(Collider body)
    {
        if (body.CompareTag(target))
        {
            body.GetComponent<IHealth>().AffectHealth(damage);

            if (explosion != null)
            {
                ParticleSystem expl = Instantiate(explosion, transform.position, Quaternion.identity);
                expl.Play();
                
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 1f;
            }
                
            Destroy(gameObject);
            
        }
    }
}
