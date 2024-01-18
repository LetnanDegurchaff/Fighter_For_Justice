using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    [SerializeField] private TargetTypes _focusType;
    
    [SerializeField] private float _speed;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _lifeTime;

    private Type _targetType;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetType = GetTargetType(_focusType);
        
        Invoke(nameof(SelfDestroy), _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_targetType == null)
            return;
        
        if (other.TryGetComponent(out Character character))
        {
            if (character.GetType() == _targetType)
            {
                other.GetComponent<Health>().ApplyDamage(RandomSystem.Range(_minDamage, _maxDamage));
                SelfDestroy();
            }
        }
    }

    public void Initialize(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speed;
    }

    private Type GetTargetType(TargetTypes targetType)
    {
        switch (targetType)
        {
            case TargetTypes.Enemy:
                return typeof(Enemy);
            
            case TargetTypes.Player:
                return typeof(Player);
        }

        return null;
    }

    private void SelfDestroy() => gameObject.SetActive(false);
    
    private enum TargetTypes
    {
        Player,
        Enemy
    }
}