using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public int startLife;
    public float timeToDestroy;
    
    [SerializeField] private int _currentLife;

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
    public void Death()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
