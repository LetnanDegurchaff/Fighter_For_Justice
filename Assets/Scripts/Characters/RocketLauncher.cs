using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RocketSpawner))]
public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 12;
    
    [Header("Rockets")]
    [SerializeField] private int _rocketPoolCapacity = 3;
    [SerializeField] private float _rocketSelfDestroyTimer = 5;

    private WaitForSeconds _delay;

    private RocketSpawner _rocketSpawner;
    private Transform _transform;
    private Type _targetType;

    private void Awake()
    {
        _delay = new WaitForSeconds(_rocketSelfDestroyTimer);
        _rocketSpawner = GetComponent<RocketSpawner>();
        _transform = transform;
    }

    private void OnEnable()
    {
        _rocketSpawner.InitializePool(_rocketPoolCapacity);
    }

    public void ShootRocket(Vector3 direction)
    {
        Quaternion rocketQuaternion = 
            Quaternion.LookRotation(direction, Vector3.up);

        if (rocketQuaternion == Quaternion.identity)
            return;

        _rocketSpawner.TrySpawnRocket
            (_transform.position, rocketQuaternion, direction.normalized, out Rocket rocket);

        StartCoroutine(DestroyRocket(rocket));
    }

    public void Rotate(Vector3 targetPosition)
    {
        Vector3 aimDirection = (_transform.position - targetPosition);

        _transform.localRotation = Quaternion.Lerp
            (_transform.localRotation,
            Quaternion.LookRotation(aimDirection),
            _rotationSpeed * Time.deltaTime);
    }

    public IEnumerator DestroyRocket(Rocket rocket)
    {
        yield return _delay;
        
        rocket.gameObject.SetActive(false);
    }
}