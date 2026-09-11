using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiObject;
    [SerializeField] private Image _fillImage;

    public void SetVisible(bool value)
    {
        _uiObject?.SetActive(value);
    }

    public void SetStaminaFill(float value, float maxValue)
    {
        if (_fillImage == null) return;
        _fillImage.fillAmount = value / maxValue;
    }
}
