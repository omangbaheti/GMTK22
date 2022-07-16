using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject[] enemies;
    
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void Spawn()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)]);
    }
}
