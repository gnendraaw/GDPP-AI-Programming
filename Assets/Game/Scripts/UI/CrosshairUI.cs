using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private Image _crosshairImage;
    [SerializeField] private Color _highlightColor = Color.yellowNice;
    [SerializeField] private Color _restColor = Color.white;

    public void SetHighlight(bool value)
    {
        _crosshairImage.color = value ? _highlightColor : _restColor;
    }
}
