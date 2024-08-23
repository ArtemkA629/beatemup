using UnityEngine;

public class EntityAnimator
{
    private readonly Animator _animator;

    public EntityAnimator(Animator animator)
    {
        _animator = animator; 
    }

    public void TryPlayWalking(float xMovement, float zMovement)
    {
        if (xMovement == 0f && zMovement == 0f)
            _animator.SetBool("IsWalking", false);
        else
            _animator.SetBool("IsWalking", true);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger("Attack");
    }

    public bool IsAttacking()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }

    public void PlayDead()
    {
        _animator.SetTrigger("Dead");
    }
}
