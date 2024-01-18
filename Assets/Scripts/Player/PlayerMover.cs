using System;
using System.Collections;
using UnityEngine;

public class PlayerMover : CharacterMover
{
    [Header("Dash")]
    [SerializeField, Range(1,7)] private int _maxDashAmount = 3;
    [SerializeField] private float _dashSpeed = 20;
    [SerializeField] private float _dashCooldown = 1;

    private Coroutine _dashRecovery;
    private WaitForSeconds _dashRecoveryDuration;

    public event Action<int, float> DashAmoutChenged;

    public int DashAmout { get; private set; }
    public int MaxDashAmout => _maxDashAmount;

    private void Start()
    {
        _dashRecoveryDuration = new WaitForSeconds(_dashCooldown);        
    }

    private void OnEnable()
    {
        if (_dashRecovery != null)
            StopCoroutine(_dashRecovery);

        UpdateDashAmount(_maxDashAmount);
    }

    private void OnDisable()
    {
        if (_dashRecovery != null)
            StopCoroutine(_dashRecovery);
    }

    public void Dash(Vector2 direction)
    {
        if (direction.magnitude == 0)
            return;

        if (DashAmout == 0)
            return;

        UpdateDashAmount(-1);
        SetVelocity(direction * _dashSpeed);
        StartDashRecovery();
    }

    private void StartDashRecovery()
    {
        if (_dashRecovery != null)
            StopCoroutine(_dashRecovery);

        _dashRecovery = StartCoroutine(RecoveryDash());
    }

    private IEnumerator RecoveryDash()
    {
        do
        {
            yield return _dashRecoveryDuration;

            UpdateDashAmount();
        } while (DashAmout < _maxDashAmount);
    }

    private void UpdateDashAmount(int addedDashAmount = 1)
    {
        DashAmout += addedDashAmount;
        DashAmout = Mathf.Clamp(DashAmout, 0, _maxDashAmount);
        DashAmoutChenged?.Invoke(DashAmout, _dashCooldown);
    }
}