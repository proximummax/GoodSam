using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : HealthComponent
{
    private Ragdoll _ragdoll;
    private PlayerWeaponOwnerComponent _weaponOwner;
    private AimingComponent _aiming;
    [SerializeField] private Volume _volume;
    [SerializeField] private CameraManager _cameraManager;
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
    }

    protected override void OnDamage()
    {
        Vignette vignette;
       
        if (_volume.profile.TryGet(out vignette))
        {
            float percent = 1.0f - (_currentHealth / _initHealth);
            vignette.intensity.value = percent * 0.5f;
        }
    }
}
