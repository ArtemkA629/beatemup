using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [SerializeField] private HealthView _healthView;
    [SerializeField] private float _destroyDelay = 2f;

    public void Init()
    {
        base.OnEnable();
        Movement = new PlayerMovement(transform, Speed, EntityAnimator, MinXBound, MaxXBound, MinZBound, MaxZBound);
    }

    protected override void Update()
    {
        base.Update();
        if (PlayerInput.IsAttacking())
            Attack();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        SceneManager.LoadScene(0);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        _healthView.OnHealthChanged();
    }

    protected override void Attack()
    {
        if (IsAttacking)
            return;

        if (GetMinEnemyDistance(out Enemy enemy) <= DistanceToAttack)
            enemy.TakeDamage(AttackDamage);
        base.Attack();
    }

    protected override void OnHealthChanged()
    {
        if (Health.Amount != 0)
            return;

        base.OnHealthChanged();
    }

    public override void Destroy()
    {
        base.Destroy();
        Destroy(this, _destroyDelay);
    }

    private float GetMinEnemyDistance(out Enemy nearestEnemy)
    {
        float minDistance = float.MaxValue;
        nearestEnemy = null;

        foreach (var enemy in EnemyPool.ActiveEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance >= minDistance)
                continue;

            minDistance = distance;
            nearestEnemy = enemy;
        }

        return minDistance;
    }
}
