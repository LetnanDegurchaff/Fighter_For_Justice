using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _value = 1;
    [SerializeField] private int _maxValue = 100;

    public event Action<int,int> ValueChanged;

    public int Value => _value;
    public int MaxValue => _maxValue;

    private void Start()
    {
        ChangeValue(_maxValue);
    }

    public void ApplyDamage(int damage) => ChangeValue(_value - damage);

    public void Heal(int heal) => ChangeValue(_value + heal);

    private void ChangeValue(int newValue)
    {
        _value = Mathf.Clamp(newValue, 0, _maxValue);
        ValueChanged?.Invoke(_value, _maxValue);

        if (_value == 0)
            gameObject.SetActive(false);
    }
}