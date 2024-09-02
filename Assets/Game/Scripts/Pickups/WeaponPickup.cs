using UnityEngine;

public class WeaponPickup : BasePickup
{
    [SerializeField] private BaseWeapon _weaponPrefab;
    private void OnTriggerEnter(Collider other)
    {
     //   print("enter?");
        if (other.TryGetComponent(out PlayerWeaponOwnerComponent weaponOwner))
        {
            BaseWeapon weapon = Instantiate(_weaponPrefab);
            weaponOwner.EquipWeapon(weapon);
            Destroy(gameObject);
        }

        if (other.TryGetComponent(out AIWeaponOwner aiWeaponOwner))
        {
            print("enter?");
            if (aiWeaponOwner.GetActiveWeapon() != null)
                return;

            BaseWeapon weapon = Instantiate(_weaponPrefab);
            aiWeaponOwner.EquipWeapon(weapon);
            Destroy(gameObject);

        }

    }
}
