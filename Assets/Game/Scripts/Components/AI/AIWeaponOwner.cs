using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponOwner : BaseWeaponOwnerComponent
{
    public override void EquipWeapon(BaseWeapon weapon)
    {
        base.EquipWeapon(weapon);
    }
    public void DropWeapon()
    {
        var activeWeapon = GetActiveWeapon();
        if (activeWeapon)
        {
            activeWeapon.transform.SetParent(null);
            activeWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            activeWeapon.gameObject.AddComponent<Rigidbody>();
            ResetWeapon();
        }
    }
}
