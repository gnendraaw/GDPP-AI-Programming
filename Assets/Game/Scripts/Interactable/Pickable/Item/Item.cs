using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    public UnityEvent OnItemPicked;

    [SerializeField] private ItemData _itemData;

    public string Name => _itemData.Name;

    [ContextMenu("Interact")]
    public void Interact(PlayerCharacter character)
    {
        Pickup(character);
    }

    public virtual void Pickup(PlayerCharacter  character)
    {
        ItemData itemData = new ItemData(_itemData.ID, _itemData.Name);
        character.Inventory.AddItem(itemData);
        
        OnItemPicked?.Invoke();
        Destroy(gameObject);
    }
}
