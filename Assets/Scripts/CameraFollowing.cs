using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _minXBound = 2f;
    [SerializeField] private float _maxXBound = 38f;

    private Vector3 _startPosition;

    private void OnEnable()
    {
        _startPosition = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    private void LateUpdate()
    {
        var cameraWorldPosition = _player.transform.position + _startPosition;
        var desiredPosition = new Vector3(_startPosition.x, cameraWorldPosition.y, cameraWorldPosition.z);
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _speed);
        smoothedPosition.z = Mathf.Clamp(smoothedPosition.z, _minXBound, _maxXBound);
        transform.position = smoothedPosition;
    }
}
