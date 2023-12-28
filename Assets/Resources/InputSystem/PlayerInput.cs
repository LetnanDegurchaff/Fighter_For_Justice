using System;
using UnityEngine;

public class PlayerInput
{
    private InputSystem _playerInput;

    private bool _isAiming = false;

    public event Action StartedAim;
    public event Action Shooted;
    public event Action Dashed;

    public PlayerInput(InputSystem playerInput)
    {
        _playerInput = playerInput;
    }

    public void Enable()
    {
        _playerInput.Enable();
        _playerInput.Player.Shoot.performed += ctx => StartAim();
        _playerInput.Player.Shoot.canceled += ctx => Shoot();
        _playerInput.Player.Dash.performed += ctx => Dash();
    }

    public void Disable()
    {
        _playerInput.Disable();
        _playerInput.Player.Shoot.performed -= ctx => StartAim();
        _playerInput.Player.Shoot.canceled -= ctx => Shoot();
        _playerInput.Player.Dash.performed -= ctx => Dash();
    }

    public Vector2 GetMoveVector()
    {
        if (_isAiming)
            return Vector2.zero;

        return _playerInput.Player.Move.ReadValue<Vector2>();
    }

    private void StartAim()
    {
        StartedAim?.Invoke();
        _isAiming = true;
    }

    private void Shoot()
    {
        Shooted?.Invoke();
        _isAiming = false;
    }

    private void Dash()
    {
        Dashed?.Invoke();
    }
}