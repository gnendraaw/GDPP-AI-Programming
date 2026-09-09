using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<ItemData> _items = new();
    public List<ItemData> Items => _items;

    public void AddItem(ItemData item)
    {
        _items.Add(item);
    }

    public void RemoveItem(ItemData item)
    {
        _items.Remove(item);
    }

    public bool CheckItem(string id)
    {
        bool isExist = Items.Exists(itemData => string.Equals(itemData.ID, id));
        return isExist;
    }
}
