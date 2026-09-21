using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventBase : MonoBehaviour
{
    public UnityEvent OnEventTriggered;
    public UnityEvent OnEventFinished;
    
    [SerializeField] private string _id;
    [SerializeField] private bool _isOneTime;
    
    public string ID => _id;
    public bool IsOneTime => _isOneTime;

    public void Start()
    {
        GameEventManager.Instance.RegisterEvent(this);
    }

    public virtual void Trigger()
    {
        OnEventTriggered?.Invoke();
    }
    
    public virtual void Finish()
    {
        OnEventFinished?.Invoke();
        
        if (!_isOneTime) return;
        GameEventManager.Instance.UnregisterEvent(this);
        Destroy(gameObject);
    }
}