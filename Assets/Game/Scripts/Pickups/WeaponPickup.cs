using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private BaseWeapon _weaponPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out WeaponOwnerComponent weaponOwner))
        {
            BaseWeapon weapon = Instantiate(_weaponPrefab);
            weaponOwner.EquipWeapon(weapon);
        }
       
    }
}
