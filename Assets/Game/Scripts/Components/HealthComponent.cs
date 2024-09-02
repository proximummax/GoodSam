
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class HealthComponent : MonoBehaviour
{
    [SerializeField] private Transform _meshRoot;
    [SerializeField] protected BaseHealthBar _healthBar;
    [SerializeField] protected float _initHealth;
    protected float _currentHealth;


   [SerializeField] private HitEffect _hitEffect;

    public UnityEvent OnDied;

    private void Start()
    {
        SetSubscribeStateOnHitsEvents(true);
        _currentHealth = _initHealth;
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
        foreach (var rigidbody in rigidbodies)
        {
            if (rigidbody.gameObject.TryGetComponent(out HitBox hitBox))
            {
                if (state)
                    hitBox.OnHit += TakeDamage;
                else
                    hitBox.OnHit -= TakeDamage;
            }
        }

        OnStart();
    }
    public void TakeDamage(float amount)
    {
        if (_currentHealth <= 0)
            return;

        _currentHealth -= amount;
        if (_healthBar)
        {
            _healthBar.SetPercentage(_currentHealth / _initHealth);
        }

        OnDamage();
        if (_currentHealth <= 0.0f)
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
        return _currentHealth <= 0.0f;
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Min(_initHealth, _currentHealth + amount);
        if (_healthBar)
        {
            _healthBar.SetPercentage(_currentHealth / _initHealth);
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
    protected virtual void OnHeal(float amount) {
    }

}
