using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyController : MonoBehaviour, IHealth
{
    [SerializeField] private float[] healthValues;
    [SerializeField] private int[] speedValues;
    
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
    private GameObject[] _enemies;
    
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
        attackRate = .1f;
        InvokeRepeating(nameof(PerformAction), attackRate, attackRate);
    }

    public void ResetEnemies()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _lookAtTarget = GetComponent<HealerEnemy>().GetClosestEnemy(_enemies);
    }
    
    private void Start()
    {
        RNGMechanic.ReRollEvent.AddListener(ChangeEnemyProperties);
        
        health = maxHealth;
        _transform = transform;

        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        InvokeRepeating($"PerformAction", attackRate, attackRate);

        _agent = GetComponent<NavMeshAgent>();
        
        _target = _lookAtTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (enemyType != EnemyType.Healer) return;
        if (transform.GetComponent<HealerEnemy>() == null)
            transform.AddComponent<HealerEnemy>();
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
                print(healers[0].GetComponent<RangedEnemyController>()._enemies.Length);
            }
        }
    }
}

