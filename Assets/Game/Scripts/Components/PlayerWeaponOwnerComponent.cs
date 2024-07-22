using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponOwnerComponent : BaseWeaponOwnerComponent
{
    [Header("UI")]
    [SerializeField] private WidgetsHolder _widgetsHolder;

    public void Holster(InputAction.CallbackContext context)
    {
        bool isHolstered = _rigAnimator.GetBool("holster_weapon");
        if (isHolstered)
            StartCoroutine(ActivateWeapon(_currentWeaponIndex));
        else
            StartCoroutine(HolsterWeapon(_currentWeaponIndex));

    }
  
    public override void EquipWeapon(BaseWeapon weapon)
    {
        base.EquipWeapon(weapon);
        weapon.SetRecoil(weapon.GetComponent<BaseRecoil>());
        weapon.Init(_aimingComponent, _rigAnimator);
    }

    protected override void OnAmmoChanged(int ammo, int clips)
    {
        _widgetsHolder.AmmoWidget.Refresh(ammo, clips);
    }
}
