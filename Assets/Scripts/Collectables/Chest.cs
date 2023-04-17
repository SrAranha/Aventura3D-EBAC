using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Chest : MonoBehaviour
{
    [Header("Items")]
    public GameObject itemToDrop;
    public ItemType itemType;
    public int quantityToDrop;
    public Transform dropPoint;
    public Vector2 randomDropArea;
    public Vector3 dropArea;
    [Header("Items Animation")]
    public float itemDropDuration;
    [Header("Chest Animation")]
    public GameObject chestTop;
    public float chestAnimDur;
    public Vector3 rotationClose;
    public Vector3 rotationOpen;
    [Header("Interaction")]
    public GameObject interactionIcon;
    public bool lookAtCamera;

    private bool _isOpened;
    private Inputs _inputs;
    private Camera _mainCamera;
    private bool _playerInRange;
    private List<GameObject> _items = new();
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
            AudioManager.instance.PlaySFXByType(SFXType.CHEST);
            chestTop.transform.DOLocalRotate(rotationOpen, chestAnimDur);
            interactionIcon.SetActive(false);
            _isOpened = true;
            DropItems();
        }
    }
    [NaughtyAttributes.Button]
    private void CloseChest()
    {
        if (!_isOpened) return;
        chestTop.transform.DOLocalRotate(rotationClose, chestAnimDur);
        _isOpened = false;
    }
    private void DropItems()
    {
        for (int i = 0; i < quantityToDrop; i++)
        {
            var item = Instantiate(itemToDrop);
            var newPos = transform.position + dropArea * Random.Range(randomDropArea.x, randomDropArea.y);
            newPos.y += 1f;
            item.transform.position = newPos;
            _items.Add(item);
        }
        StartCoroutine(AnimateItems());
    }
    IEnumerator AnimateItems()
    {
        yield return new WaitForSeconds(chestAnimDur);
        foreach (var item in _items)
        {
            item.transform.DOMove(item.transform.position + Vector3.up, itemDropDuration);
            item.transform.DOScale(Vector3.zero, itemDropDuration);
            yield return new WaitForSeconds(itemDropDuration);
            InventoryManager.instance.AddItemByType(itemType);
            Destroy(item);
        }
        _items.Clear();
    }
}
