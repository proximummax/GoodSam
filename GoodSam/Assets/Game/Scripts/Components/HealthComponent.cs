using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _initHealth;
    private float _currentHealth;

    private Ragdoll _ragdoll;

    public UnityAction Died;
    private void Start()
    {
        SetSubscribeStateOnHitsEvents(true);
        _ragdoll = GetComponent<Ragdoll>();
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
        if (_currentHealth <= 0.0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Died?.Invoke();
        _ragdoll.ActivateRagdoll();
    }
}
