using UnityEngine;

public class AimTargetCalculater : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 0.2f;

    private PlayerInput _playerInput;
    
    private Transform _transform;
    private Vector3 _startAimPoint;
    private Vector3 _currentAimPoint;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_playerInput == null)
            return;
        
        Vector2 lookVector = _playerInput.GetLookVector();

        _currentAimPoint +=
            new Vector3(lookVector.x, lookVector.y, 0);
    }

    public void SetPlayerInput(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }
    
    public void StartNewCalculation()
    {
        _startAimPoint = Input.mousePosition;
        _currentAimPoint = _startAimPoint;
    }
    
    /*public Vector3 GetAimTarget() =>
        GetAimDirection() * _sensitivity + _transform.position;

    public Vector3 GetAimDirection()
    {
        return GetPointerPosition() - _startAimPoint;
    }*/
    
    public Vector3 GetAimTarget() =>
        GetAimDirection() * _sensitivity + _transform.position;

    public Vector3 GetAimDirection()
    {
        return _currentAimPoint - _startAimPoint;
    }

    private Vector3 GetPointerPosition()
    {
        return Input.mousePosition;
    }
}