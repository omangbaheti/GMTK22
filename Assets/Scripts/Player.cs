using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour, IHealth
{
    [SerializeField] private float hitPoints = 500f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform weaponMount;
    [SerializeField] private List<GameObject> armory;
    [SerializeField] private Animator player;
    [SerializeField] private float gravity;
    
    private CharacterController character;
    private CapsuleCollider characterCollider;
    private static readonly int Speed = Animator.StringToHash("speed");
    private Vector3 gravityVector;

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
        characterCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        //ApplyGravity();
    }

    void ApplyGravity()
    {
        if (character.isGrounded)
        {
            gravityVector = new Vector3(0, 0, 0);
            return;
        }
        gravityVector = new Vector3(0f, gravity, 0f);
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
        Vector3 movement = new Vector3(movementDirection.x, -9.81f, movementDirection.z);
        character.Move(speed * Time.deltaTime * movement);
        player.SetFloat(Speed, movementDirection.magnitude);
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

    private void OnDestroy()
    {
        SceneManager.LoadScene($"GameOver");
    }
}
