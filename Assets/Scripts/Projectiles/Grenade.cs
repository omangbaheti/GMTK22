using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Grenade : Projectile
{
    [SerializeField] private float upwardThrust;
    [SerializeField] private ParticleSystem explosionVFX;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    
    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Rigidbody bulletRigidBody = GetComponent<Rigidbody>();
        bulletRigidBody.AddForce(transform.forward * bulletSpeed + transform.up * upwardThrust, ForceMode.Impulse);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Debug.Log("destroyed");
            ParticleSystem expl = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 1f;
            expl.Play();
            Destroy(gameObject);
        }
    }

}
