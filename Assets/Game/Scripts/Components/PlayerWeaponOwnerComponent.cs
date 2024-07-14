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
    }

    protected override void OnAmmoChanged(int ammo)
    {
        _widgetsHolder.AmmoWidget.Refresh(ammo);
    }
}
