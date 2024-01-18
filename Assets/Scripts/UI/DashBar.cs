using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    private Coroutine _fillingBar;

    public void Initialization(int dashAmount)
    {
        Image circle = GetComponentInChildren<Image>();

        for (int i = 1; i < dashAmount; i++)
        {
            Instantiate(circle, circle.transform.position, circle.transform.rotation, transform);
        }

        RectTransform rectTransform = GetComponent<RectTransform>();
        Rect rect = rectTransform.rect;
        float width = rectTransform.sizeDelta.x;

        rectTransform.sizeDelta = new Vector2(width * dashAmount, rectTransform.sizeDelta.y);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x - width / 2 * (dashAmount - 1), rectTransform.localPosition.y, 0);

        _images = GetComponentsInChildren<Image>();

        foreach (Image image in _images)
            image.fillAmount = 1;
    }

    public void OnDashAmoutChenged(int dashAmount, float dashRecoveryDuration)
    {
        foreach (Image image in _images)
            image.fillAmount = 0;

        for (int i = 0; i < dashAmount; i++)
            _images[i].fillAmount = 1;

        if (dashAmount == _images.Length)
            return;

        if (_fillingBar != null)
            StopCoroutine(_fillingBar);

        _fillingBar = StartCoroutine(FillingBar(dashAmount, dashRecoveryDuration));

        for (int i = dashAmount; i < _images.Length; i++)
            _images[i].fillAmount = 0;
    }

    private IEnumerator FillingBar(int barIndex, float fillingDuration)
    {
        float elapsedTime = 0;

        while (_images[barIndex].fillAmount < 1)
        {
            _images[barIndex].fillAmount = elapsedTime / fillingDuration;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}