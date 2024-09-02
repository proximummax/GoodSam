using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponOwnerComponent : BaseWeaponOwnerComponent
{
    [Header("UI")]
    [SerializeField] private WidgetsHolder _widgetsHolder;
    [SerializeField] private BaseWeapon[] _weapons;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(StartSequence());
       
    }
    private IEnumerator StartSequence()
    {
        foreach (var weaponPrefab in _weapons)
        {
            BaseWeapon weapon = Instantiate(weaponPrefab);
            EquipWeapon(weapon);
            yield return new WaitForSeconds(0.1f);
        }
    }

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

    protected override void OnAmmoChanged(int ammo)
    {
        _widgetsHolder.AmmoWidget.Refresh(ammo);
    }
}
