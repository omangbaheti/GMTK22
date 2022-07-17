using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float[] spawnRates;
    
    [SerializeField] private GameObject[] enemies;
    
    private float _spawnRate;

    private void Awake()
    {
        RNGMechanic.ReRollEvent.AddListener(SpawnerReRoll);
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), _spawnRate, _spawnRate);
    }

    private void Spawn()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)]);
        HealerEnemy[] healers = FindObjectsOfType<HealerEnemy>();

        foreach (var healer in healers)
            healer.GetComponent<RangedEnemyController>().ResetEnemies();
    }

    private void SpawnerReRoll(int number)
    {
        _spawnRate = spawnRates[number];
    }
}
