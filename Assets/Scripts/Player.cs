using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHealth
{
    [SerializeField] private float hitPoints = 500f;
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

    public void AffectHealth(float changeInHealth)
    {
        if (hitPoints > 0)
        {
            hitPoints -= changeInHealth;
            if (hitPoints.Equals(0) || hitPoints < 0)
            {
                Debug.Log($"{hitPoints}");
                Destroy(gameObject);
            }
        }
    }
}
