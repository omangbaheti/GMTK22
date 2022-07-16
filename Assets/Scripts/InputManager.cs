using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector3> MovementDirection;
    public static event Action Shoot;
    public static event Action<Vector3> ShootingVector; 
    
    private Vector2 shootingDirection;
    private bool shoot;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        PollMovementDirection();
        PollShootDirection();
        PollShoot();
    }
    

    private void PollMovementDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movementDirection = new Vector3(horizontal,0f, vertical);
        MovementDirection?.Invoke(movementDirection);
    }

    private void PollShootDirection()
    {
        Ray mouseCoordinates = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseCoordinates, out RaycastHit hit))
        {
            Vector3 shootDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
            ShootingVector.Invoke(shootDirection);
        }
    }
    

    private void PollShoot()
    {
        if(Input.GetKey(KeyCode.Mouse0))
            Shoot?.Invoke();
    }
}
