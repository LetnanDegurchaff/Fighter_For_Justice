using System;
using System.Collections;
using UnityEngine;

public class AimTargetCalculater : MonoBehaviour
{
    [SerializeField] private LayerMask _aimSurfaceMask;

    private Camera _camera;
    private float _raycastDistance = 100;
    private Transform _transform;

    private Vector3 _startAimPoint;

    private Coroutine _coroutine;

    private void Awake()
    {
        _camera = Camera.main;
        _transform = transform;
    }

    public void StartRotate(Action<Vector3> changeTarget)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(RotateToAimTarget(changeTarget));
    }

    public void StopRotate()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void SetStartAimPoint()
    {
        _startAimPoint = GetPointerPosition();
    }

    public Vector3 GetAimTarget()
    {
        return GetPointerPosition() - _startAimPoint + _transform.position;
    }

    public Vector3 GetAimDirection()
    {
        return GetPointerPosition() - _startAimPoint;
    }

    private Vector3 GetPointerPosition()
    {
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, _raycastDistance, _aimSurfaceMask);
        return hitInfo.point;
    }

    private IEnumerator RotateToAimTarget(Action<Vector3> changeTarget)
    {
        while (true)
        {
            changeTarget(GetAimTarget());

            yield return null;
        }
    }
}