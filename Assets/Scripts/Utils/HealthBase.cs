using System;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public int startingHealth;
    public bool destroyOnDeath;
    public float timeToDestroy;

    public Action<HealthBase> OnDeath;

    [Header("DEBUG")]
    [SerializeField] private int _currentHealth;
    [SerializeField] private bool _isAlive = true;
    [SerializeField] private Collider _collider;

    private void OnValidate()
    {
        _collider = GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }
    public void ResetHealth()
    {
        _currentHealth = startingHealth;
    }
    public void Damage(int damage)
    {
        if (_isAlive)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }
    private void Kill()
    {
        _isAlive = false;
        _collider.enabled = false;
        if (destroyOnDeath)
        {
            Destroy(gameObject, timeToDestroy);
        }
        OnDeath?.Invoke(this);
    }
    public void Revive()
    {
        _isAlive = true;
        _collider.enabled = true;
        ResetHealth();
    }
    public bool IsAlive()
    { 
        return _isAlive;
    }
}
