using System.Collections;
using UnityEngine;

public class CharacterRotater : MonoBehaviour
{
    private AimTargetCalculater _aimTargetCalculater;
    
    private Transform _transform;
    private RocketLauncher _launcher;

    private Transform _target;
    private Vector3 _targetPosition;

    private Coroutine _coroutine;

    private void Awake()
    {
        _aimTargetCalculater = GetComponent<AimTargetCalculater>();
        _transform = transform;
        _launcher = GetComponentInChildren<RocketLauncher>();
        
        SetNeutralRotation();
    }

    private void Update()
    {
        if (_target != null)
        {
            _targetPosition = _target.position;
        }

        _launcher.Rotate(_targetPosition);
    }

    public void ChangeTarget(Transform target)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
            
        _target = target;
    }

    public void ChangeTarget()
    {
        _coroutine = StartCoroutine(KeepTrackTarget());
    }

    public void SetNeutralRotation()
    {
        ChangeTarget(_transform);
    }

    private IEnumerator KeepTrackTarget()
    {
        _target = null;

        while (true)
        {
            _targetPosition = _aimTargetCalculater.GetAimTarget();

            yield return null;
        }
    }
}