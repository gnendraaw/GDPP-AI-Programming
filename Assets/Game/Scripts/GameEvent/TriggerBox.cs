using UnityEngine;
using UnityEngine.Events;

public class TriggerBox : MonoBehaviour
{
    public UnityEvent OnTriggered;
    
    [SerializeField] private bool _autoActive = true;
    [SerializeField] private bool _isOneTime = false;
    [SerializeField] private string _tag = "Player";

    private bool _isActive = false;

    private void Start()
    {
        _isActive = _autoActive;
    }

    public void SetActive(bool value)
    {
        _isActive = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;
        if (!other.CompareTag(_tag)) return;
        
        OnTriggered?.Invoke();
        
        if (_isOneTime) Destroy(gameObject);
    }
}
