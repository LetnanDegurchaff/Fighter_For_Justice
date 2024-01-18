using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _movingSpeedModifier;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        if (_playerTransform != null)
            _transform.position = Vector3.Lerp
                (_transform.position,
                new Vector3(_playerTransform.position.x, _playerTransform.position.y, _transform.position.z),
                _movingSpeedModifier * Time.deltaTime);
    }
}