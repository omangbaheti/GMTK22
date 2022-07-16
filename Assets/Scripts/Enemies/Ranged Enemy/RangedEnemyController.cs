using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyController : MonoBehaviour, IHealth
{
    [SerializeField] private float[] healthValues;
    [SerializeField] private int[] speedValues;
    [SerializeField] private float[] attackRates;
    
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float range;
    [SerializeField] private float minRange;
    [SerializeField] private float attackRate;
    [SerializeField] private float moveForwardSpeed;
    [SerializeField] private float moveBackSpeed;
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform bulletDirection;
    
    private Transform _lookAtTarget;
    private Transform _target;
    private Transform _transform;
    private bool _inRange;
    private NavMeshAgent _agent;
    private bool _moveBack;
    private float _distance;
    
    private enum EnemyType
    {
        Healer, Ranged, Melee
    }
    private void ChangeEnemyProperties(int number)
    {
        if (number < 3)
            health -= healthValues[number];
        else
            health += healthValues[number];

        moveForwardSpeed = speedValues[number];

        CancelInvoke(nameof(PerformAction));
        attackRate = attackRates[number];
        InvokeRepeating(nameof(PerformAction), attackRate, attackRate);
    }
    
    private void Start()
    {
        RNGMechanic.ReRollEvent.AddListener(ChangeEnemyProperties);
        
        health = maxHealth;
        _transform = transform;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        InvokeRepeating($"PerformAction", attackRate, attackRate);

        _agent = GetComponent<NavMeshAgent>();
        
        _target = _lookAtTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (enemyType != EnemyType.Healer) return;
        if (transform.GetComponent<HealerEnemy>() == null)
            transform.AddComponent<HealerEnemy>();
        _lookAtTarget = GetComponent<HealerEnemy>().GetClosestEnemy(enemies);
    }

    private void Update()
    {
        _transform.LookAt(_target);

        if (enemyType == EnemyType.Healer)
        {
            transform.GetChild(0).LookAt(_lookAtTarget);   
        }
        _moveBack = _distance < minRange;
        _distance = Vector3.Distance(_transform.position, _target.position);
        _inRange = _distance <= range;

        if (_moveBack)
            transform.Translate(Vector3.back * moveBackSpeed * Time.deltaTime);

        if (_inRange)
        {
            _agent.speed = 0f;
            return;
        }

        _agent.speed = moveForwardSpeed;
        _agent.destination = _target.position;
    }

    private void PerformAction()
    {
        if (!_inRange) return;
        Instantiate(bullet, _transform.position, bulletDirection.rotation);
    }

    void IHealth.AffectHealth(float changeInHealth)
    {
        if (enemyType != EnemyType.Healer)
        {
            if (health > 0)
                health -= changeInHealth;
            else
                Destroy(gameObject);
        }
        else
        {
            if (health < maxHealth)
                health += changeInHealth;
        }
    }
}
