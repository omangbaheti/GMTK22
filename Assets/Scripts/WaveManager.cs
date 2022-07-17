using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int enemiesKilled;
    public int enemiesSpawned;
        
    [SerializeField] private GameObject spawners;

    public void EndWave()
    {
        spawners.SetActive(false);
    }
    
    public void ChangeWave()
    {
        spawners.SetActive(true);
        StartCoroutine(nameof(StartWave));
    }
    
    private void Start()
    {
        StartCoroutine(nameof(StartWave));
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(10f);
        enemiesSpawned = 0;
        spawners.SetActive(true);
    }
}
