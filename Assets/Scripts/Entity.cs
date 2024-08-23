using System.Collections;
using UnityEngine;
using Zenject;

public class Entity : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private float _minXBound = -2f;
    [SerializeField] private float _maxXBound = 0.5f;
    [SerializeField] private float _minZBound = -1f;
    [SerializeField] private float _maxZBound = 41f;
    [SerializeField] private float _maxHealthAmount = 100f;
    [SerializeField] private float _attackDamage = 10f;
    [SerializeField] private float _distanceToAttack = 0.5f;

    [Inject] private EnemyPool _enemyPool;

    private EntityAnimator _entityAnimator;
    private EntityMovement _movement;
    private Health _health;
    private bool _isAttacking;
    private bool _isDead;

    public float Speed => _speed;
    public float MinXBound => _minXBound;
    public float MaxXBound => _maxXBound;
    public float MinZBound => _minZBound;
    public float MaxZBound => _maxZBound;
    public float MaxHealthAmount => _maxHealthAmount;
    public float AttackDamage => _attackDamage;
    public float DistanceToAttack => _distanceToAttack;
    public EntityAnimator EntityAnimator => _entityAnimator;
    public EntityMovement Movement { get => _movement; protected set => _movement = value; }
    public Health Health => _health;
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public bool IsDead { get => _isDead; protected set => _isDead = value; }
    public EnemyPool EnemyPool => _enemyPool;

    protected virtual void OnEnable()
    {
        _entityAnimator = new(_animator);
        _health = new(_maxHealthAmount);
        _health.Changed += OnHealthChanged;
    }

    protected virtual void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
    }

    protected virtual void Update()
    {
        _movement.Move();
    }

    public virtual void TakeDamage(float damage)
    {
        _health.ApplyDamage(damage);
    }

    protected virtual void Attack()
    {
        if (_isAttacking)
            return;

        _entityAnimator.PlayAttack();
    }

    protected virtual void OnHealthChanged()
    {
        if (Health.Amount != 0)
            return;

        _entityAnimator.PlayDead();
        Destroy();
    }

    public virtual void Destroy()
    {
        _isDead = true;
    }
}
