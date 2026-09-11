using UnityEngine;
using TMPro;

public class InteractionInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiObject;
    [SerializeField] private TMP_Text _nameText;

    public void SetVisible(bool value)
    {
        _uiObject.SetActive(value);
    }

    public void SetText(string value)
    {
        _nameText.text = value;
    }
}
