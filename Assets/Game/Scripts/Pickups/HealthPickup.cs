using UnityEngine;

public class HealthPickup : BasePickup
{
    [SerializeField] private float _amount = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseHealthComponent health))
        {
            health.Heal(_amount);
            Destroy(gameObject);
        }
    }
}