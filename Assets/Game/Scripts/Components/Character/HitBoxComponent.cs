using UnityEngine;
using UnityEngine.Events;

public class HitBoxComponent : MonoBehaviour
{
    [SerializeField] private float _damageMultiplier = 1.0f;

    public UnityAction<float> OnHit;
    public void ApplyHit(float projectileDamage)
    {
        OnHit?.Invoke(_damageMultiplier * projectileDamage);
    }
}
