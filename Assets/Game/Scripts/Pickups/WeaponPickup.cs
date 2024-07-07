using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private BaseWeapon _weaponPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerWeaponOwnerComponent weaponOwner))
        {
            BaseWeapon weapon = Instantiate(_weaponPrefab);
            weaponOwner.EquipWeapon(weapon);
            Destroy(gameObject);
        }
        
        if (other.TryGetComponent(out AIWeaponOwner aiWeaponOwner))
        {
            BaseWeapon weapon = Instantiate(_weaponPrefab);
            aiWeaponOwner.EquipWeapon(weapon);
            Destroy(gameObject);

        }

    }
}
