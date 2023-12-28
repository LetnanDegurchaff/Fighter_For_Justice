using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterRotater))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(ShootSystem))]
public class PlayerController : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerInput _inputSystem;
    private ShootSystem _shootSystem;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _shootSystem = GetComponent<ShootSystem>();
        _inputSystem = new PlayerInput(new InputSystem());
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
        _inputSystem.StartedAim += _shootSystem.Aim;
        _inputSystem.Shooted += _shootSystem.Shoot;
        _inputSystem.Dashed += Dash;
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
        _inputSystem.StartedAim -= _shootSystem.Aim;
        _inputSystem.Shooted -= _shootSystem.Shoot;
        _inputSystem.Dashed -= Dash;
    }

    private void FixedUpdate()
    {
        _playerMover.Move(_inputSystem.GetMoveVector());
    }

    private void Dash()
    {
        _playerMover.Dash(_inputSystem.GetMoveVector());
    }
}