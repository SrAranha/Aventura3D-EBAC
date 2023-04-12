using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    [Min(0)]
    public int id;

    private bool _isActive;
    private string _key;
    private MeshRenderer _meshRenderer;
    private int _lastCheckpoint;

    private void OnValidate()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    private void Awake()
    {
        CheckpointOff();
    }
    private void Start()
    {
        _key = CheckpointManager.instance.checkpointKey;
    }
    public void CheckpointOn()
    {
        _isActive = true;
        _meshRenderer.material.SetColor("_EmissionColor", Color.white);
        SaveCheckpoint();
    }
    public void CheckpointOff()
    {
        _isActive = false;
        _meshRenderer.material.SetColor("_EmissionColor", Color.black);
        PlayerPrefs.SetInt(_key, id -1); // Desse modo, o último checkpoint é o anterior, mesmo se o jogador não tiver pego ele.
    }
    private void SaveCheckpoint()
    {
        _lastCheckpoint = PlayerPrefs.GetInt(_key);
        if (_lastCheckpoint < id)
        {
            PlayerPrefs.SetInt(_key, id);
        }
        _lastCheckpoint = PlayerPrefs.GetInt(_key);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive)
        {
            if (other.CompareTag("Player"))
            {
                CheckpointOn();
            }
        }
    }
}
