using UnityEngine;
using UnityEngine.Events;


public class BaseHealthComponent : MonoBehaviour
{
    [SerializeField] private Transform _meshRoot;
    [SerializeField] protected BaseHealthBar _healthBar;
    [SerializeField] protected float _initHealth;
    [SerializeField] private HitEffect _hitEffect;

    protected float CurrentHealth;
    public UnityEvent OnDied;

    private void Start()
    {
        SetSubscribeStateOnHitsEvents(true);
        CurrentHealth = _initHealth;
    }

    private void OnDisable()
    {
        SetSubscribeStateOnHitsEvents(false);
    }

    public void SetHealth(float health)
    {
        _initHealth = health;
    }

    private void SetSubscribeStateOnHitsEvents(bool state)
    {
        var rigidbodies = _meshRoot.GetComponentsInChildren<Rigidbody>();
        foreach (var rbody in rigidbodies)
        {
            if (!rbody.gameObject.TryGetComponent(out HitBoxComponent hitBox)) continue;
            
            if (state)
                hitBox.OnHit += TakeDamage;
            else
                hitBox.OnHit -= TakeDamage;
        }

        OnStart();
    }

    private void TakeDamage(float amount)
    {
        if (CurrentHealth <= 0)
            return;

        CurrentHealth -= amount;
        if (_healthBar)
        {
            _healthBar.SetPercentage(CurrentHealth / _initHealth);
        }

        OnDamage();
        if (CurrentHealth <= 0.0f)
        {
            Die();
        }

        _hitEffect.Apply();
    }

    private void Die()
    {
        OnDeath();
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0.0f;
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(_initHealth, CurrentHealth + amount);
        if (_healthBar)
        {
            _healthBar.SetPercentage(CurrentHealth / _initHealth);
        }

        OnHeal(amount);
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnDeath()
    {
    }

    protected virtual void OnDamage()
    {
    }

    protected virtual void OnHeal(float amount)
    {
    }
}