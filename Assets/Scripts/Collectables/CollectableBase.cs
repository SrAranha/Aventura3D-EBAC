using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public ItemType itemType;
    public float timeToHide;
    public GameObject model;
    [Header("Spinning")]
    public Vector3 spinDirection;
    public float spinSpeed;
    [Header("VFX")]
    public ParticleSystem vfx;

    [Header("DEBUG")]
    [SerializeField] private Collider _collider;
    private readonly string _playerTag = "Player";

    private void OnValidate()
    {
        if (_collider == null) _collider = GetComponent<Collider>();
    }
    private void Update()
    {
        SpinCollectable();
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
        StartCoroutine(HideObject());
        vfx.Play();
        OnCollectItem();
    }
    IEnumerator HideObject()
    {
        model.SetActive(false);
        yield return new WaitForSeconds(timeToHide);
        Destroy(gameObject);
    }
    private void OnCollectItem()
    {
        InventoryManager.instance.AddItemByType(itemType);
    }
    private void SpinCollectable()
    {
        transform.RotateAround(transform.position, spinDirection, spinSpeed * Time.deltaTime);
    }
}
