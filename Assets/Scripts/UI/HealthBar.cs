using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image _image;
    private Text _text;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<Text>();
    }

    public void UpdateView(int value, int maxValue)
    {
        _image.fillAmount = (float) value / maxValue;
        _text.text = value.ToString();
    }
}