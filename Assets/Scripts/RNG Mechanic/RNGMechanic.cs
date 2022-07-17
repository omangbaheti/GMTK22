using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class RNGMechanic : MonoBehaviour
{
    public static readonly UnityEvent<int> ReRollEvent = new UnityEvent<int>();
    
    [SerializeField] private int maxNumber;
    [SerializeField] private WaveManager waveManager;

    private int _number;
    
    private void Start()
    {
        ReRoll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReRoll();
            waveManager.Start();
        }
    }

    public void ReRoll()
    {
        _number = Random.Range(1, maxNumber + 1);
        ReRollEvent.Invoke(_number - 1);
    }
}
