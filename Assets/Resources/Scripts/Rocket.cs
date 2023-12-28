using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private int _minDamage = 10;
    [SerializeField] private int _maxDamage = 50;
    [SerializeField] private int _lifeTime = 20;

    private Type _targetType;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Invoke(nameof(SelfDestroy), _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_targetType == null)
            return;

        if (other.TryGetComponent(out Character character))
            if (character.GetType() == _targetType)
            {
                other.GetComponent<Health>().ApplyDamage(RandomSystem.Range(_minDamage, _maxDamage));
                SelfDestroy();
            }
    }

    public void Initialize(Vector3 direction, Type targetType)
    {
        _targetType = targetType;
        _rigidbody.velocity = direction * _speed;
    }

    private void SelfDestroy() => gameObject.SetActive(false);
}