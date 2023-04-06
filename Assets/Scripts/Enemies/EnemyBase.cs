using DG.Tweening;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public int startLife;
    [SerializeField] private int _currentLife;
    public float timeToDestroy;

    [Header("Animation")]
    public Animator animator;

    [SerializeField] private Collider _collider;
    [SerializeField] protected EnemyPathWalk _pathWalk;

    private void OnValidate()
    {
        _collider = GetComponent<Collider>();
        _pathWalk = GetComponent<EnemyPathWalk>();
    }
    private void Start()
    {
        Init();
    }
    protected virtual void Init()
    {
        ResetLife();
    }

    public void Damage(int damage)
    {
        _currentLife -= damage;

        if (_currentLife <= 0 )
        {
            transform.DOKill();
            Death();
        }
    }
    private void Death()
    {
        if (_pathWalk != null)
        {
            _pathWalk.StopWalking(isIdle: false);
            Destroy(_pathWalk.walkpoints[0].parent.gameObject);
        }
        _collider.enabled = false;
        PlayAnimationByTrigger("Death");
        Destroy(gameObject, timeToDestroy);
    }
    private void ResetLife()
    {
        _currentLife = startLife;
    }
    public void PlayAnimationByTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
