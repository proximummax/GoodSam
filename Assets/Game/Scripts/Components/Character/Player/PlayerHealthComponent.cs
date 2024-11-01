using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealthComponent : BaseHealthComponent
{
    [SerializeField] private Volume _volume;
    [SerializeField] private CameraManager _cameraManager;
    
    private Ragdoll _ragdoll;
    private PlayerWeaponOwnerComponent _weaponOwner;
    private AimingComponent _aiming;
    protected override void OnStart()
    {
        _ragdoll = GetComponent<Ragdoll>();
        _weaponOwner = GetComponent<PlayerWeaponOwnerComponent>();
        _aiming = GetComponent<AimingComponent>();
    }
    protected override void OnDeath()
    {
        _ragdoll.ActivateRagdoll();
        _weaponOwner.DropWeapon();
        _aiming.enabled = false;
        _cameraManager.EnableKillCamera();


        OnDied?.Invoke();
    }

    protected override void OnDamage()
    {
       UpdateVignette();
    }
 
    protected override void OnHeal(float amount)
    {
        UpdateVignette();
    }
    private void UpdateVignette()
    {
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            float percent = 1.0f - (CurrentHealth / _initHealth);
            vignette.intensity.value = percent * 0.5f;
        }
    }
}
