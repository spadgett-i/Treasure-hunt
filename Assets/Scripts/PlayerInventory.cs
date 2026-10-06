using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<string> items = new List<string>();

    public void AddItem(string itemName)
    {
        if (!items.Contains(itemName))
        {
            items.Add(itemName);

            Debug.Log("Objeto añadido al inventario: " + itemName);
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName);
        }
    }

    public int GetItemCount()
    {
        return items.Count;
    }
}