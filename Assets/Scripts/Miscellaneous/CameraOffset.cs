using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    [SerializeField] private float innerRadius= 1f;
    [SerializeField] private float outerRadius = 4f;

    private void Start()
    {
        InputManager.PlayerFacingDirection += CheckCameraOffset;
        
    }

    private void Update()
    {
        
    }

    private void CheckCameraOffset(Vector3 shootingDirection)
    {
        transform.position = transform.parent.position + shootingDirection.normalized *
                                  Mathf.Clamp(shootingDirection.magnitude, innerRadius, outerRadius);
    }
}
