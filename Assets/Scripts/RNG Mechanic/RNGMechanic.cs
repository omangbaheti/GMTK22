using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class RNGMechanic : MonoBehaviour
{
    public static readonly UnityEvent<int> ReRollEvent = new UnityEvent<int>();
    
    [SerializeField] private int maxNumber;

    private int _number;
    
    private void Start()
    {
        ReRoll();
    }

    private void ReRoll()
    {
        _number = Random.Range(1, maxNumber + 1);
        ReRollEvent.Invoke(_number);
    }
}
