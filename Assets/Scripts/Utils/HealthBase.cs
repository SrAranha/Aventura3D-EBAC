using System;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public int startingHealth;
    public bool destroyOnDeath;
    public float timeToDestroy;
    [Header("Health Bar")]
    public bool hasHealthBar;
    public HealthBarUI healthBarUpdater;
    [Header("Healing VFX")]
    public ParticleSystem healingVFX;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnDeath;

    [Header("DEBUG")]
    [SerializeField] private int _currentHealth;
    private bool _isAlive = true;
    private Collider _collider;

    private void OnValidate()
    {
        _collider = GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }
    private void Update()
    {
        UpdateHealthBar();
    }
    public void ResetHealth()
    {
        _currentHealth = startingHealth;
    }
    public void Heal()
    {
        if (InventoryManager.instance.RemoveItemByType(ItemType.LIFE_PACK))
        {
            healingVFX.Play();
            ResetHealth();
        }
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
            OnDamage?.Invoke(this);
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
    [NaughtyAttributes.Button]
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
    private void UpdateHealthBar()
    {
        if (hasHealthBar)
        {
            healthBarUpdater.UpdateValue((float)_currentHealth / startingHealth);
        }
    }
}
