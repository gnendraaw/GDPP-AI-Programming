using UnityEngine;
using UnityEngine.UI;

public class BatteryLevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiObject;
    [SerializeField] private Color _highColor = Color.green;
    [SerializeField] private Color _lowColor = Color.red;
    [SerializeField] private Image _batteryFill;

    public void SetVisible(bool value)
    {
        _uiObject?.SetActive(value);
    }

    public void UpdateBatteryFill(float value, float maxValue)
    {
        float fillAmount = value / maxValue;
        _batteryFill.fillAmount = fillAmount;
        
        Color useColor = Color.Lerp(_lowColor, _highColor, fillAmount);
        _batteryFill.color = useColor;
    }
}