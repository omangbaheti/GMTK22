using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform weaponMount;
    [SerializeField] private List<GameObject> armory;
    
    private CharacterController character;

    private void OnEnable()
    {
        InputManager.MovementDirection += Movement;
        InputManager.PlayerFacingDirection += RotatePlayer;
        RNGMechanic.ReRollEvent.AddListener(EquipWeapon);
    }

    private void RotatePlayer(Vector3 facingDirection)
    {
        transform.forward = facingDirection;
    }

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    private void EquipWeapon(int weaponIndex)
    {
        GameObject weaponToEquip = armory[weaponIndex % armory.Count];
        foreach (Transform child in weaponMount.transform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(weaponToEquip, weaponMount);
    }
    private void Movement(Vector3 movementDirection)
    {
        character.Move(speed * Time.deltaTime * movementDirection);
    }
    
    
    private void OnDisable()
    {
        InputManager.MovementDirection -= Movement;
        RNGMechanic.ReRollEvent.RemoveListener(EquipWeapon);
    }
}
