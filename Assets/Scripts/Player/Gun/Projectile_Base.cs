using UnityEngine;

public class Projectile_Base : MonoBehaviour
{
    [Header("Projectile_Base")]
    public int damage;
    public float projectileSpeed;
    public float timeToDestroy = 2f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        gameObject.transform.Translate(projectileSpeed * Time.deltaTime * Vector3.forward);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        damageable?.Damage(damage);
        Destroy(gameObject);
    }
}
