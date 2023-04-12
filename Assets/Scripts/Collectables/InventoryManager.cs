using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{ 
    COIN,
    LIFE_PACK
}
public class InventoryManager : Singleton<InventoryManager>
{
    public List<ItemSetup> itemSetups;

    public void AddItemByType(ItemType type, int amount = 1)
    {
        if (amount < 1) return; 
        var item = itemSetups.Find(i => i.itemType == type);
        item.inventory.quantity += amount;
        Debug.Log("Added " + amount + "x " + type + " to inventory");
    }
    public void RemoveItemByType(ItemType type, int amount = 1)
    {
        if (amount > 0) return;
        var item = itemSetups.Find(i => i.itemType == type);
        item.inventory.quantity -= amount;
        Debug.Log("Removed " + amount + "x " + type + " from inventory");
    }
    [NaughtyAttributes.Button]
    public void ResetInventory()
    {
        foreach(var item in itemSetups)
        {
            item.inventory.quantity = 0;
        }
    }
    [NaughtyAttributes.Button]
    private void AddCoin()
    {
        AddItemByType(ItemType.COIN, 1);
    }
    [NaughtyAttributes.Button]
    private void AddLifePack()
    {
        AddItemByType(ItemType.LIFE_PACK, 1);
    }
}
[System.Serializable]
public class ItemSetup
{
    public ItemType itemType;
    public SO_Inventory inventory;
}