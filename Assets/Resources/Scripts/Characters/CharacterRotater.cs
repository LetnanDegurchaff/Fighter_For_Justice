using UnityEngine;

public class CharacterRotater : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private int _rotationAimAngle = 90;

    private Transform _transform;
    private Transform _launcher;

    private Transform _target;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _transform = transform;
        _launcher = GetComponentInChildren<RocketLauncher>().transform;
        SetNeutralRotation();
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            _targetPosition = _target.position;
        } 

        RotateLauncher(_targetPosition);
    }

    public void ChangeTarget(Transform target) => _target = target;

    public void ChangeTarget(Vector3 targetPosition)
    {
        _target = null;
        _targetPosition = targetPosition;
    }

    public void SetNeutralRotation() => _target = _transform;

    private void RotateLauncher(Vector3 targetPosition)
    {
        Vector3 aimDirection = (_transform.position - targetPosition);

        _launcher.localRotation = Quaternion.Lerp
            (_launcher.localRotation,
            Quaternion.LookRotation(aimDirection),
            _rotationSpeed * Time.deltaTime);

        //Quaternion.Euler(Mathf.Atan2(aimDirection.y, Mathf.Abs(aimDirection.x)) * Mathf.Rad2Deg, 0, 0),
    }
}