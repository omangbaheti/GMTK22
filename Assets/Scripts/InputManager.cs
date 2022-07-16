using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector3> MovementDirection;
    public static event Action Shoot;
    private Vector2 shootingDirection;
    private bool shoot;
    
   
    void Update()
    {
        PollMovementDirection();
        PollShoot();
    }

    private void PollMovementDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movementDirection = new Vector3(horizontal,0f, vertical);
        MovementDirection?.Invoke(movementDirection);
    }

    private void PollShoot()
    {
        if(Input.GetKey(KeyCode.Mouse0))
            Shoot?.Invoke();
    }
}
