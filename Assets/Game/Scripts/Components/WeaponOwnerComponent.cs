using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;


public class WeaponOwnerComponent : MonoBehaviour
{
    public enum EWeaponSlot
    {
        Primary,
        Secondary
    }

    [SerializeField] private CrosshairTarget _crosshairTarget;
    [SerializeField] private List<BaseWeapon> _weaponDatas;
    [SerializeField] private Transform[] _weaponSlots;
    [SerializeField] private ReloadComponent _reloadComponent;

    [Header("Animations")]
    [SerializeField] private Transform _leftGrip;
    [SerializeField] private Transform _rightGrip;
    [SerializeField] private Rig _handIK;


    [SerializeField] private Animator _rigAnimator;

    [SerializeField] private CinemachineFreeLook _playerCamera;

    private BaseWeapon[] _equipedWeapons = new BaseWeapon[2];
    private int _currentWeaponIndex = 0;

    private bool _isHolstered = false;

    [Header("UI")]
    [SerializeField] private WidgetsHolder _widgetsHolder;
    private void Start()
    {

        InitAnimations();
        //    _currentWeaponIndex = 0;
        //   SpawnWeapons();
    }
    private void LateUpdate()
    {
        var weapon = GetWeapon(_currentWeaponIndex);
        if (weapon && weapon.IsFiring)
        {
            if (weapon.IsFiring)
                weapon.UpdateFiring(Time.deltaTime);
            weapon.UpdateBullets(Time.deltaTime);
        }


    }
    public BaseWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= _equipedWeapons.Length)
            return null;
        return _equipedWeapons[index];
    }
    public BaseWeapon GetActiveWeapon()
    {
        return GetWeapon(_currentWeaponIndex);
    }

    public void StartFire(InputAction.CallbackContext context)
    {
        if (!CanFire()) return;
        GetWeapon(_currentWeaponIndex).StartFire();
    }

    public void StopFire(InputAction.CallbackContext context)
    {
        if (!GetWeapon(_currentWeaponIndex)) return;
        GetWeapon(_currentWeaponIndex).StopFire();

    }
    public void Holster(InputAction.CallbackContext context)
    {

        bool isHolstered = _rigAnimator.GetBool("holster_weapon");
        if (isHolstered)
            StartCoroutine(ActivateWeapon(_currentWeaponIndex));
        else
            StartCoroutine(HolsterWeapon(_currentWeaponIndex));

    }
    public void SelectWeapon(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "SelectWeapon_1":
                StartCoroutine(SwitchWeapon(_currentWeaponIndex, EWeaponSlot.Primary));
                break;
            case "SelectWeapon_2":
                StartCoroutine(SwitchWeapon(_currentWeaponIndex, EWeaponSlot.Secondary));
                break;
        }
    }

    public void NextWeapon()
    {
        if (!CanEquip())
            return;

        //      _currentWeaponIndex = (_currentWeaponIndex + 1) % _weaponDatas.Count;
        //   EquipWeapon(_currentWeaponIndex);
    }
    public void Reload(InputAction.CallbackContext context = default)
    {
        _reloadComponent.SetReloadTrigger();
         ChangeClip();
    }
    public bool GetWeaponUIData(out WeaponUIData uiData)
    {
        uiData = null;

        if (!GetWeapon(_currentWeaponIndex))
            return false;

        uiData = GetWeapon(_currentWeaponIndex).GetUIData();
        return true;
    }
    public bool GetAmmoData(out AmmoData ammoData)
    {
        ammoData = null;
        if (!GetWeapon(_currentWeaponIndex))
            return false;

        ammoData = GetWeapon(_currentWeaponIndex).GetAmmoData();
        return true;
    }

    /*    public bool TryToAddAmmo(BaseWeapon weaponType, int clipsAmount)
        {
            foreach (var weapon in _weapons)
            {
                if (!weapon || weapon.GetType() != weaponType.GetType())
                    continue;

                return weapon.TryToAddAmmo(clipsAmount);
            }
            return false;
        }
        public bool NeedAmmo(BaseWeapon weaponType)
        {
            foreach (var weapon in _weapons)
            {
                if (!weapon || weapon.GetType() != weaponType.GetType())//  !weapon->IsA(WeaponType))
                    continue;

                return weapon.IsAmmoFull();
            }
            return false;
        }*/
    protected bool CanFire()
    {
        return GetWeapon(_currentWeaponIndex) && !_isHolstered;//&& !_reloadAnimInProgress;
    }
    protected bool CanEquip()
    {
        return true;
    }
    public void EquipWeapon(BaseWeapon weapon)
    {
        if (weapon == null) return;

        if (GetWeapon(_currentWeaponIndex))
        {
            GetWeapon(_currentWeaponIndex).StopFire();
            //   Destroy(GetCurrentWeapon().gameObject);
        }
        int weaponIndex = (int)weapon.WeaponSlot;
        _equipedWeapons[weaponIndex] = weapon;


        AttachWeaponToSocket(weapon, _weaponSlots[(int)weapon.WeaponSlot]);

        StartCoroutine(SwitchWeapon(_currentWeaponIndex, (EWeaponSlot)weaponIndex));

        weapon.OnClipEmpty += OnEmptyClip;
        weapon.OnAmmoChanged += OnAmmoChanged;
        weapon.Init(_crosshairTarget, _playerCamera, _rigAnimator);
    }

    private IEnumerator SwitchWeapon(int holsterIndex, EWeaponSlot activeSlot)
    {
        if (holsterIndex == (int)activeSlot)
            holsterIndex = -1;


        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon((int)activeSlot));
        _currentWeaponIndex = (int)activeSlot;
        OnAmmoChanged( GetActiveWeapon().GetAmmoData().Bullets);
    }
    private IEnumerator HolsterWeapon(int weaponIndex)
    {
        _isHolstered = true;
        var weapon = GetWeapon(weaponIndex);
        if (weapon)
        {
            _rigAnimator.SetBool("holster_weapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (_rigAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }
    private IEnumerator ActivateWeapon(int weaponIndex)
    {
        var weapon = GetWeapon(weaponIndex);
        if (weapon)
        {
            _rigAnimator.SetBool("holster_weapon", false);
            _rigAnimator.Play("equip_" + weapon.GunAnimatorName);
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (_rigAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
        _isHolstered = false;
    }

    private void AttachWeaponToSocket(BaseWeapon weapon, Transform socket)
    {
        if (!weapon) return;

        weapon.transform.SetParent(socket, false);
    }

    private void InitAnimations()
    {
        _rigAnimator.updateMode = AnimatorUpdateMode.AnimatePhysics;
        _rigAnimator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        _rigAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        _rigAnimator.updateMode = AnimatorUpdateMode.Normal;
    }



    private bool CanReload()
    {
        return GetWeapon(_currentWeaponIndex) && GetWeapon(_currentWeaponIndex).CanReload();
    }

    private void OnEmptyClip(BaseWeapon ammoEmpty)
    {
        Debug.Log("clip is empty");
              if (!ammoEmpty)
                  return;

              if (GetActiveWeapon() == ammoEmpty)
                  Reload();
          /*    else
              {
                  foreach (var weapon in _weapons)
                  {
                      if (weapon != ammoEmpty)
                          continue;
                      weapon.ChangeClip();
                      break;
                  }
              }*/
    }
    private void ChangeClip()
    {
        if (!CanReload()) return;

        Debug.Log("can reaload");
        GetWeapon(_currentWeaponIndex).StopFire();
        GetWeapon(_currentWeaponIndex).ChangeClip();
    }
    private void OnAmmoChanged(int ammo)
    {
        _widgetsHolder.AmmoWidget.Refresh(ammo);
    }
}
