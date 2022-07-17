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
    private EnemyProperties _enemyProperties;
    private Transform _target;
    private Transform _transform;
    private bool _inRange;
    private NavMeshAgent _agent;
    private bool _moveBack;
    private GameObject[] _enemies;
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

        _enemyProperties.health = health;
        
        moveForwardSpeed = speedValues[number];
        _enemyProperties.speed = moveForwardSpeed;

        CancelInvoke(nameof(PerformAction));
        attackRate = attackRates[number];
        _enemyProperties.attackRate = attackRate;
        InvokeRepeating(nameof(PerformAction), attackRate, attackRate);
    }

    public void ResetEnemies()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Ranged Enemy");
        _lookAtTarget = GetComponent<HealerEnemy>().GetClosestEnemy(_enemies);
    }
    
    private void Start()
    {
        _enemyProperties = GameObject.FindGameObjectWithTag("Enemy Properties").GetComponent<EnemyProperties>();
        
        health = _enemyProperties.health;
        attackRate = _enemyProperties.attackRate;
        moveForwardSpeed = _enemyProperties.speed;

        RNGMechanic.ReRollEvent.AddListener(ChangeEnemyProperties);
        
        health = maxHealth;
        _transform = transform;

        _enemies = GameObject.FindGameObjectsWithTag("Ranged Enemy");
        
        InvokeRepeating($"PerformAction", attackRate, attackRate);

        _agent = GetComponent<NavMeshAgent>();
        
        _target = _lookAtTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (enemyType != EnemyType.Healer) return;
        if (transform.GetComponent<HealerEnemy>() == null)
            transform.AddComponent<HealerEnemy>();
        print(_enemies.Length);
        _lookAtTarget = GetComponent<HealerEnemy>().GetClosestEnemy(_enemies);
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        _transform.LookAt(_target);

        if (enemyType == EnemyType.Healer)
        {
            transform.GetChild(0).LookAt(_lookAtTarget);
        }
        _moveBack = _distance < minRange;
        _distance = Vector3.Distance(_transform.position, _target.position);
        _inRange = _distance <= range;

        if (_moveBack)
            transform.Translate(Vector3.back * (moveBackSpeed * Time.deltaTime));

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
        float distance = Vector3.Distance(_transform.position, _target.position);
        float distanceLookAt = 10f;
        if (_lookAtTarget != null)
            distanceLookAt = Vector3.Distance(_transform.position, _lookAtTarget.position);
        _inRange = distanceLookAt <= range;
        if (_inRange)
            Instantiate(bullet, bulletDirection.position, bulletDirection.rotation);
        _moveBack = distance < minRange;
    }

    void IHealth.AffectHealth(float changeInHealth)
    {
        if (health > 0)
        {
            health -= changeInHealth;
            if (health.Equals(0) || health < 0)
            {
                Debug.Log($"{health}");
                Destroy(gameObject);
                HealerEnemy[] healers = FindObjectsOfType<HealerEnemy>();
                foreach (var healer in healers)
                    healer.GetComponent<RangedEnemyController>().ResetEnemies();
            }
        }
    }
}

