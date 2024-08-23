using UnityEngine;
using Zenject;

public class Enemy : Entity
{
    [Inject] private Player _player;

    protected override void OnEnable()
    {
        base.OnEnable();
        Movement = new EnemyMovement(transform, _player.transform, Speed, EntityAnimator, MinXBound, MaxXBound, MinZBound, MaxZBound);
    }

    protected override void Update()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) <= DistanceToAttack && 
            IsAttacking == false && IsDead == false)
        {
            Debug.Log(23424);
            Attack();
            return;
        }

        base.Update();
    }

    public void Reset()
    {
        Health.SetAmount(MaxHealthAmount);
        IsDead = false;
    }

    protected override void Attack()
    {
        _player.TakeDamage(AttackDamage);
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
        EnemyPool.Pool.Release(this);
    }
}
