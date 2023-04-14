using UnityEngine;

public class SkinChangerBase : MonoBehaviour
{
    public SkinTypes skinType;
    public float skinDuration;

    private readonly string _playerTag = "Player";
    protected PlayerController _playerController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _playerController = other.GetComponent<PlayerController>();
            ChangeSkin();
            Destroy(gameObject);
        }
    }
    protected virtual void ChangeSkin()
    {
        SkinManager.instance.ChangeSkinByType(skinType, skinDuration);
    }
}
