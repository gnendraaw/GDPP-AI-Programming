using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent OnDoorOpened;
    public UnityEvent OnDoorClosed;
    public UnityEvent OnDoorLocked;

    [SerializeField] protected Transform _doorTransform;
    [SerializeField] protected float _duration;
    [SerializeField] protected bool _isLocked;
    [SerializeField] protected string _keyID;
    [SerializeField] protected bool _isAnimating;
    [SerializeField] protected string _name;

    [SerializeField] private bool _isOpen;

    public bool IsAnimating => _isAnimating;
    public string Name => _name;
    
    protected Coroutine _animatingDoorCoroutine;
    
    [ContextMenu("Interact Door")]
    public void Interact(PlayerCharacter character)
    {
        if (_isLocked)
        {
            bool hasKey = character.Inventory.CheckItem(_keyID);
            
            if (!hasKey)
            {
                OnDoorLocked?.Invoke();
                return;
            }
            
            _isLocked = false;
        }
        
        _isOpen = !_isOpen;
        if (_isOpen) Open();
        else Close();
        
    }

    public virtual void Open()
    {
        OnDoorOpened?.Invoke();
    }

    public virtual void Close()
    {
        OnDoorClosed?.Invoke();
    }
}
