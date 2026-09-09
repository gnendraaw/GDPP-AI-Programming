using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    public UnityEvent OnItemPicked;

    [SerializeField] private ItemData _itemData;

    public string Name => _itemData.Name;

    [ContextMenu("Interact")]
    public void Interact()
    {
        Pickup();
    }

    public void Pickup()
    {
        OnItemPicked?.Invoke();
        Destroy(gameObject);
    }
}
