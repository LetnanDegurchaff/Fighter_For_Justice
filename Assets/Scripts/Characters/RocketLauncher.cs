using System;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 12;

    private Rocket _rocket;
    private Transform _transform;
    private Type _targetType;

    private void Awake()
    {
        _transform = transform;
    }

    public void SetRocket(Rocket rocket)
    {
        _rocket = rocket;
    }
    
    public void ShootRocket(Vector3 direction)
    {
        Quaternion rocketQuaternion = 
            Quaternion.LookRotation(direction, Vector3.up);

        if (rocketQuaternion == Quaternion.identity)
            return;

        Instantiate(_rocket, _transform.position, rocketQuaternion).
            Initialize(direction.normalized);
    }

    public void Rotate(Vector3 targetPosition)
    {
        Vector3 aimDirection = (_transform.position - targetPosition);

        _transform.localRotation = Quaternion.Lerp
            (_transform.localRotation,
            Quaternion.LookRotation(aimDirection),
            _rotationSpeed * Time.deltaTime);
    }
}