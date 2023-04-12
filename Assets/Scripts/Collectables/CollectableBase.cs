using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public ItemType itemType;
    public float timeToHide;
    public GameObject model;

    private string _playerTag = "Player";
    private Collider _collider;

    private void OnValidate()
    {
        _collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            CollectItem();
        }
    }
    private void CollectItem()
    {
        _collider.enabled = false;
        Destroy(gameObject);
        OnCollectItem();
    }
    private void OnCollectItem()
    {
        InventoryManager.instance.AddItemByType(itemType);
    }
}
