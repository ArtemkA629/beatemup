using UnityEngine;

public class EnemyMovement : EntityMovement
{
    private readonly Transform _playerTransform;

    public EnemyMovement(Transform transform, Transform playerTransform, float speed, EntityAnimator animator,
        float minXBound, float maxXBound, float minZBound, float maxZBound) : 
        base(transform, speed, animator, minXBound, maxXBound, minZBound, maxZBound)  
    {
        _playerTransform = playerTransform;
    }

    public override Vector3 GetMovement()
    {
        return _playerTransform.position - Transform.position;
    }
}
