using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int enemiesKilled;
    public int enemiesSpawned;
        
    [SerializeField] private GameObject spawners;
    [SerializeField] private GameObject waveNumberUI;
    [SerializeField] private TMP_Text waveNumber;

    private int _wave;
    
    
    public void EndWave()
    {
        spawners.SetActive(false);
    }
    
    public void ChangeWave()
    {
        spawners.SetActive(true);
        StartCoroutine(nameof(StartWave));
    }
    
    public void Start()
    {
        StartCoroutine(nameof(StartWave));
    }

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(4f);
        _wave++;
        enemiesSpawned = 0;
        spawners.SetActive(true);
        waveNumberUI.SetActive(true);
        waveNumber.text = "WAVE " + _wave;
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        waveNumberUI.SetActive(false);
    }
}
