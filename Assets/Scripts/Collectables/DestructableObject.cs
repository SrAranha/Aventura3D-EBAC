using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    [Header("Items")]
    public GameObject itemToDrop;
    public int quantityToDrop;
    public Vector3 randomDropArea;
    [Header("Items Animation")]
    public float itemDropDuration;

    private HealthBase _health;
    private void OnValidate()
    {
        if (_health == null) _health = GetComponent<HealthBase>();
    }
    private void Awake()
    {
        OnValidate();
        _health.OnDeath += DropItems;
    }
    private void DropItems(HealthBase h)
    {
        for (int i = 0; i < quantityToDrop; i++)
        {
            AudioManager.instance.PlaySFXByType(SFXType.DESTRUCTABLE);
            var item = Instantiate(itemToDrop);
            var newPos = transform.position + new Vector3(1, 0, 1) * Random.Range(randomDropArea.x, randomDropArea.z);
            newPos.y += randomDropArea.y; 
            item.transform.position = newPos;
        }
    }
}
