using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayOnSpawn : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool playOnAwake;

    private void Start()
    {
        if (clip != null && playOnAwake)
        {
            SoundManager.Instance.PlaySound(clip);
        }
    }

    public void PlaySound()
    {
        if (clip != null)
        {
            SoundManager.Instance.PlaySound(clip);
        }
    }
    
}
