using System.Collections;
using UnityEngine;

public class FightArea : MonoBehaviour
{
    [SerializeField] private Shader _shader;
    [SerializeField] private float _colorChangeDuration;

    [SerializeField] private Color _alarmColor;
    [SerializeField] private Color _defaultColor;

    private Material _material;

    private Coroutine _changingColor;

    private void Awake()
    {
        _material = new Material(_shader);
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
            renderer.material = _material;

        _material.color = _defaultColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_changingColor != null)
            StopCoroutine(_changingColor);

        _changingColor = StartCoroutine(ChangingColor(_alarmColor));
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_changingColor != null)
            StopCoroutine(_changingColor);

        _changingColor = StartCoroutine(ChangingColor(_defaultColor));
    }

    private IEnumerator ChangingColor(Color targetColor)
    {
        float elapsedTime = 0;

        while (_material.color != targetColor)
        {
            _material.color = Color.Lerp(_material.color, targetColor, elapsedTime / _colorChangeDuration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}