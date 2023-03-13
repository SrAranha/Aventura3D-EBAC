using UnityEngine;

public class Projectile_Base : MonoBehaviour
{
    [Header("Projectile_Base")]
    public int damage;
    public float projectileSpeed;
    public float timeToDestroy = 2f;

    //private AudioSource audioSource;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.Play();
    }
    private void Update()
    {
        gameObject.transform.Translate(projectileSpeed * Time.deltaTime * Vector3.forward);
    }
}
