using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private CharacterController character;

    private void OnEnable()
    {
        InputManager.MovementDirection += Movement; 
    }

    void Start()
    {
        character = GetComponent<CharacterController>();
    }
    

    private void Movement(Vector3 movementDirection)
    {
        character.Move(speed * Time.deltaTime * movementDirection);
    }
    
    
    private void OnDisable()
    {
        InputManager.MovementDirection -= Movement;
    }
}
