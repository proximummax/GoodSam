using UnityEngine;

public class AmmoPickup : BasePickup
{
    [SerializeField] private int _clipAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BaseWeaponOwnerComponent weaponOwner))
        {
            weaponOwner.TryToAddAmmo(_clipAmount);
        }
    }
}
