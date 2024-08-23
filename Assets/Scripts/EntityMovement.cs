using UnityEngine;

public abstract class EntityMovement : IMovable
{
    private readonly Transform _transform;
    private readonly float _speed;
    private readonly EntityAnimator _animator;
    private readonly float _minXBound;
    private readonly float _maxXBound;
    private readonly float _minZBound;
    private readonly float _maxZBound;

    public Transform Transform => _transform;

    public EntityMovement(Transform transform, float speed, EntityAnimator animator,
        float minXBound, float maxXBound, float minZBound, float maxZBound)
    {
        _transform = transform;
        _speed = speed;
        _animator = animator;
        _minXBound = minXBound;
        _maxXBound = maxXBound;
        _minZBound = minZBound;
        _maxZBound = maxZBound;
    }

    public virtual void Move()
    {
        Vector3 movement = GetMovement();

        float startXPosition = _transform.position.x;
        float startZPosition = _transform.position.z;
        ApplyMovement(movement);
        Rotate(_transform.position.z - startZPosition);
        _animator.TryPlayWalking(_transform.position.x - startXPosition, _transform.position.z - startZPosition);
    }

    private void ApplyMovement(Vector3 movement)
    {
        _transform.Translate(_speed * Time.deltaTime * movement, Space.World);
        ClampPosition();
    }

    private void Rotate(float xMovement)
    {
        if (xMovement == 0f)
            return;

        float yEuler = xMovement < 0f ? 180f : 0f;
        _transform.rotation = new Quaternion(_transform.rotation.x, yEuler, _transform.rotation.z, _transform.rotation.w);
    }

    private void ClampPosition()
    {
        _transform.position = new Vector3(Mathf.Clamp(_transform.position.x, _minXBound, _maxXBound), _transform.position.y,
            Mathf.Clamp(_transform.position.z, _minZBound, _maxZBound));
    }

    public abstract Vector3 GetMovement();
}
