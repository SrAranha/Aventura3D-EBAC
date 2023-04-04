using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public int startLife;
    [SerializeField] private int _currentLife;
    public float timeToDestroy;

    [Header("Animation")]
    public Animator animator;

    private Collider _collider;

    private void OnValidate()
    {
        _collider = GetComponent<Collider>();
    }
    private void Start()
    {
        _currentLife = startLife;
    }

    public void Damage(int damage)
    {
        _currentLife -= damage;

        if (_currentLife <= 0 )
        {
            Death();
        }
    }
    private void Death()
    {
        _collider.enabled = false;
        PlayAnimationByTrigger("Death");
        Destroy(gameObject, timeToDestroy);
    }
    private void PlayAnimationByTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
