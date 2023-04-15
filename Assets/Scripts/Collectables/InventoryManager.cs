using System.Collections.Generic;

public enum ItemType
{ 
    COIN,
    LIFE_PACK
}
public class InventoryManager : Singleton<InventoryManager>
{
    public InventoryUI inventoryUI;
    public List<ItemSetup> itemSetups;

    private void Start()
    {
        SaveManager.instance.LoadCollectables();
    }
    public void AddItemByType(ItemType type, int amount = 1)
    {
        if (amount < 1) return; 
        var item = itemSetups.Find(i => i.itemType == type);
        item.inventory.quantity += amount;
        inventoryUI.UpdateUI(type);
        SaveManager.instance.SaveCollectables();
    }
    public bool RemoveItemByType(ItemType type, int amount = 1)
    {
        var item = itemSetups.Find(i => i.itemType == type);
        if (item.inventory.quantity > 0 && amount <= item.inventory.quantity)
        {
            item.inventory.quantity -= amount;
            inventoryUI.UpdateUI(type);
            SaveManager.instance.SaveCollectables();
            return true;
        }
        else return false;
    }
    public ItemSetup GetItemByType(ItemType type)
    {
        return itemSetups.Find(i => i.itemType == type);
    }
    [NaughtyAttributes.Button]
    public void ResetInventory()
    {
        foreach(var item in itemSetups)
        {
            item.inventory.quantity = 0;
        }
        inventoryUI.UpdateUI();
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