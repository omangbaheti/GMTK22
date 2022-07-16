using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class RNGMechanic : MonoBehaviour
{
    public UnityEvent reRollEvent = new UnityEvent();
    
    [SerializeField] private int maxNumber;

    private int _number;
    
    private void Start()
    {
        ReRoll();
    }

    private void ReRoll()
    {
        _number = Random.Range(1, maxNumber + 1);
        reRollEvent.Invoke();
    }
}
