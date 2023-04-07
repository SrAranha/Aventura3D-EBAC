using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [Header("Projectile Base")]
    public int damage;
    public float projectileSpeed;
    public float timeToDestroy = 2f;
    public string[] tagsToHit;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
    private void Update()
    {
        gameObject.transform.Translate(projectileSpeed * Time.deltaTime * Vector3.forward);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with => " + collision.gameObject);
        foreach (var t in tagsToHit)
        {
            if (collision.transform.CompareTag(t))
            {
                var damageable = collision.gameObject.GetComponent<IDamageable>();
                damageable?.Damage(damage);
            }
        }
        Destroy(gameObject);
    }
}
