using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyController : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float range;
    [SerializeField] private float minRange;
    [SerializeField] private float attackRate;
    [SerializeField] private float moveForwardSpeed;
    [SerializeField] private float moveBackSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private Transform bullet;

    private Transform _transform;
    private bool _inRange;
    private NavMeshAgent _agent;
    private bool _moveBack;
    
    private enum EnemyType
    {
        Healer, Ranged, Melee
    }
    
    private void Start()
    {
        health = maxHealth;
        _transform = transform;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        InvokeRepeating($"PerformAction", attackRate, attackRate);

        _agent = GetComponent<NavMeshAgent>();
        
        if (enemyType != EnemyType.Healer) return;
        if (transform.GetComponent<HealerEnemy>() == null)
            transform.AddComponent<HealerEnemy>();
        target = GetComponent<HealerEnemy>().GetClosestEnemy(enemies);
    }

    private void Update()
    {
        _transform.LookAt(target);

        if (_moveBack)
            transform.Translate(Vector3.back * moveBackSpeed * Time.deltaTime);

        if (_inRange)
        {
            _agent.speed = 0f;
            return;
        }

        _agent.speed = moveForwardSpeed;
        _agent.destination = target.position;
    }

    private void PerformAction()
    {
        float distance = Vector3.Distance(_transform.position, target.position);
        _inRange = distance <= range;
        if (!_inRange) return;
        Instantiate(bullet, _transform.position, _transform.rotation);
        _moveBack = distance < minRange;
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
