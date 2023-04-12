using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public List<InventoryUISetup> inventoryItems;
    private InventoryManager manager;
    public void UpdateUI()
    {
        foreach (var item in inventoryItems)
        {
            UpdateUI(item.itemType);
        }
    }
    public void UpdateUI(ItemType type)
    {
        var itemToUpdate = inventoryItems.Find(i => i.itemType == type);
        itemToUpdate.tmp_Text.text = manager.GetItemByType(type).inventory.quantity.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        manager = InventoryManager.instance;
        UpdateUI();
    }
}
[System.Serializable]
public class InventoryUISetup
{
    public ItemType itemType;
    public TMP_Text tmp_Text;
}
