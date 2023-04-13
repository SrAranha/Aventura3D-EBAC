using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Chest : MonoBehaviour
{
    [Header("Items")]
    public GameObject itemToDrop;
    public int quantityToDrop;
    public Transform dropPoint;
    public Vector2 randomDropArea;
    [Header("Items Animation")]
    public float itemDropDuration;
    [Header("Chest Animation")]
    public GameObject chestTop;
    public float animationDuration;
    public Vector3 rotationClose;
    public Vector3 rotationOpen;
    [Header("Interaction")]
    public GameObject interactionIcon;
    public bool lookAtCamera;

    private bool _isOpened;
    private Inputs _inputs;
    private Camera _mainCamera;
    private bool _playerInRange;
    [SerializeField] private List<GameObject> _items = new();
    private string playerTag = "Player";
    private void OnEnable()
    {
        _inputs?.Enable();
    }
    private void OnDisable()
    {
        _inputs?.Disable();
    }
    private void Awake()
    {
        _inputs = new Inputs();
        _inputs.Enable();
    }
        // Start is called before the first frame update
    void Start()
    {
        if (_mainCamera == null) _mainCamera = FindObjectOfType<Camera>();
        interactionIcon.SetActive(false);
        _inputs.Gameplay.OpenChest.performed += ctx => OpenChest();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            _playerInRange = true;
            interactionIcon.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            _playerInRange = false;
            interactionIcon.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (lookAtCamera)
        {
            interactionIcon.transform.LookAt(_mainCamera.transform);
        }
    }
    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_playerInRange)
        {
            if (_isOpened) return;
            chestTop.transform.DOLocalRotate(rotationOpen, animationDuration);
            interactionIcon.SetActive(false);
            _isOpened = true;
            DropItems();
        }
    }
    [NaughtyAttributes.Button]
    private void CloseChest()
    {
        if (!_isOpened) return;
        chestTop.transform.DOLocalRotate(rotationClose, animationDuration);
        _isOpened = false;
    }
    private void DropItems()
    {
        for (int i = 0; i < quantityToDrop; i++)
        {
            var item = Instantiate(itemToDrop, dropPoint);
            item.transform.position = dropPoint.position + Vector3.forward * Random.Range(randomDropArea.x, randomDropArea.y);
            _items.Add(item);
        }
        StartCoroutine(AnimateItems());
    }
    IEnumerator AnimateItems()
    {
        foreach (var item in _items)
        {
            item.transform.DOMove(item.transform.position + Vector3.up, itemDropDuration);
            item.transform.DOScale(Vector3.zero, itemDropDuration);
            yield return new WaitForSeconds(itemDropDuration);
            InventoryManager.instance.AddItemByType(ItemType.COIN);
            Destroy(item);
        }
        _items.Clear();
    }
}
