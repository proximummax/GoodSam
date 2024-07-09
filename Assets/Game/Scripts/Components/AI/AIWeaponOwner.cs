using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponOwner : BaseWeaponOwnerComponent
{
    private WeaponIK _weaponIK;

    protected override void Start()
    {
        base.Start();
        _weaponIK = GetComponent<WeaponIK>();
    }
    public override void EquipWeapon(BaseWeapon weapon)
    {
        base.EquipWeapon(weapon);
    }
    public void SetTarget(Transform target)
    {
        _weaponIK.SetTargetTransform(target);
    }
    public IEnumerator ActivateWeapon()
    {
        yield return new WaitForSeconds(0.5f);
        _weaponIK.SetAimTransform(GetActiveWeapon().MuzzleSocket);
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
