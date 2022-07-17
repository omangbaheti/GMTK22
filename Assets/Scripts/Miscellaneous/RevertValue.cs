using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RevertValue : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    
    private void Start()
    {
        _cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
            Mathf.Lerp(_cinemachineBasicMultiChannelPerlin.m_AmplitudeGain, 0f, 10f * Time.deltaTime);
    }
}
