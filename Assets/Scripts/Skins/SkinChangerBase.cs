using UnityEngine;

public class SkinChangerBase : MonoBehaviour
{
    [Header("Spinning")]
    public Vector3 spinDirection;
    public float spinSpeed;
    [Header("Skin Changer")]
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
            AudioManager.instance.PlaySFXByType(SFXType.COLLECTABLE);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        SpinCollectable();
    }
    protected virtual void ChangeSkin()
    {
        SkinManager.instance.ChangeSkinByType(skinType, skinDuration);
    }
    private void SpinCollectable()
    {
        transform.RotateAround(transform.position, spinDirection, spinSpeed * Time.deltaTime);
    }
}
