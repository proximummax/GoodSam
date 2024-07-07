using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private AIHealthBar _healthBar;
    [SerializeField] private float _initHealth;
    private float _currentHealth;


    private HitEffect _hitEffect;

    public UnityAction Died;
    private void Start()
    {
        SetSubscribeStateOnHitsEvents(true);

        _hitEffect = GetComponent<HitEffect>();
        _currentHealth = _initHealth;
    }
    private void OnDisable()
    {
        SetSubscribeStateOnHitsEvents(false);
    }
    private void SetSubscribeStateOnHitsEvents(bool state)
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
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
    }
    public void TakeDamage(float amount)
    {
        if (_currentHealth <= 0)
            return;

        _currentHealth -= amount;
        _healthBar.SetPercentage(_currentHealth / _initHealth);
        if (_currentHealth <= 0.0f)
        {
            Die();
        }
        _hitEffect.Apply();
    }
    private void Die()
    {
        Died?.Invoke();
    }
}
