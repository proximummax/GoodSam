using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : BasePickup
{

    [SerializeField] private float _amount = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HealthComponent health))
        {
            health.Heal(_amount);
            Destroy(gameObject);
        }
    }
}
