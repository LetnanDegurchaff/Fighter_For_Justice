using System;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private Rocket _rocket;

    private Transform _transform;
    private AimTargetCalculater _aimCalculater;
    private Type _targetType;

    private void Awake()
    {
        _transform = transform;
        _aimCalculater = GetComponentInParent<AimTargetCalculater>();
    }

    public void SetRocketTargetType(Type type) => _targetType = type;

    public void ShootRocket(Vector3 direction)
    {
        Quaternion rocketQuaternion = Quaternion.LookRotation(direction, Vector3.up);

        if (rocketQuaternion == Quaternion.identity)
            return;

        Instantiate(_rocket, _transform.position, rocketQuaternion).
            Initialize(direction.normalized, _targetType);
    }
}