using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float range;
    [SerializeField] private float attackRate;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private Transform bullet;

    private Transform _transform;
    private bool _inRange;
    
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

        if (enemyType == EnemyType.Healer)
        {
            if (transform.GetComponent<HealerEnemy>() == null)
                transform.AddComponent<HealerEnemy>();
            target = GetComponent<HealerEnemy>().GetClosestEnemy(enemies);
        }

        print(target.name);
    }

    private void Update()
    {
        _transform.LookAt(target);
        if (_inRange) return;
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void PerformAction()
    {
        _inRange = Vector3.Distance(_transform.position, target.position) <= range;
        if (!_inRange) return;
        Instantiate(bullet, _transform.position, _transform.rotation);
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
