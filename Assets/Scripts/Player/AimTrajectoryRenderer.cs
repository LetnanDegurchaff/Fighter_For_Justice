using System.Collections;
using UnityEngine;

public class AimTrajectoryRenderer : MonoBehaviour
{
    private AimTargetCalculater _aimTargetCalculater;
    private LineRenderer _lineRenderer;
    private Transform _transform;
    
    private Coroutine _rendering;

    private void Awake()
    {
        _aimTargetCalculater = GetComponent<AimTargetCalculater>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _transform = transform;

        _lineRenderer.enabled = false;
    }

    public void StartRendering()
    {
        if (_rendering != null)
            StopCoroutine(_rendering);

        _rendering = StartCoroutine(Rendering());
    }

    public void StopRendering()
    {
        StopCoroutine(_rendering);
        HideTrajectory();
    }

    private IEnumerator Rendering()
    {
        _aimTargetCalculater.StartNewCalculation();
        _lineRenderer.enabled = true;

        while (true)
        {
            ShowTrajectory(_aimTargetCalculater.GetAimTarget());

            yield return null;
        }
    }

    public void ShowTrajectory(Vector3 direction)
    {
        _lineRenderer.SetPositions(new Vector3[] { _transform.position, direction });
    }

    public void HideTrajectory() => _lineRenderer.enabled = false;
}