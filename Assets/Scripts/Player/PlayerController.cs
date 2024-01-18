using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterRotater))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(ShootSystem))]
[RequireComponent(typeof(AimTrajectoryRenderer))]
[RequireComponent(typeof(AimTargetCalculater))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    
    private PlayerMover _playerMover;
    private PlayerInput _playerInput;
    private ShootSystem _shootSystem;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _shootSystem = GetComponent<ShootSystem>();
        _playerInput = new PlayerInput(new InputSystem());
        GetComponent<AimTargetCalculater>().SetPlayerInput(_playerInput);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.StartedAim += _shootSystem.Aim;
        _playerInput.Shooted += _shootSystem.Shoot;
        _playerInput.Dashed += Dash;
    }
    
    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.StartedAim -= _shootSystem.Aim;
        _playerInput.Shooted -= _shootSystem.Shoot;
        _playerInput.Dashed -= Dash;
    }
    
    private void FixedUpdate()
    {
        _playerMover.Move(_playerInput.GetMoveVector());
    }
    
    private void Dash()
    {
        _playerMover.Dash(_playerInput.GetMoveVector());
    }
}