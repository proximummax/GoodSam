using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponOwnerComponent : MonoBehaviour
{
    [SerializeField] private List<WeaponData> _weaponDatas;
    [SerializeField] private Transform _weaponEquipSocket;
    [SerializeField] private Transform _armoryEquipSocket;
    [SerializeField] private Animation _equipAnimation;

    private BaseWeapon _currentWeapon = null;
    private List<BaseWeapon> _weapons = new List<BaseWeapon>();
    private int _currentWeaponIndex = 0;

    private Animation _currentReloadAnim = null;
    private bool _equipAnimInProgress = false;
    private bool _reloadAnimInProgress = false;


    private void Start()
    {
        enabled = false;

        InitAnimations();
        _currentWeaponIndex = 0;
        SpawnWeapons();
        EquipWeapon(_currentWeaponIndex);
    }
    public void StartFire(InputAction.CallbackContext context)
    {
        if (!CanFire()) return;
        _currentWeapon.StartFire();
    }
    public void StopFire(InputAction.CallbackContext context)
    {
        if (!_currentWeapon) return;
        _currentWeapon.StopFire();
    }
    public void NextWeapon()
    {
        if (!CanEquip())
            return;

        _currentWeaponIndex = (_currentWeaponIndex + 1) % _weaponDatas.Count;
        EquipWeapon(_currentWeaponIndex);
    }
    public void Reload()
    {
        ChangeClip();
    }
    public bool GetWeaponUIData(out WeaponUIData uiData)
    {
        uiData = null;

        if (!_currentWeapon)
            return false;

        uiData = _currentWeapon.GetUIData();
        return true;
    }
    public bool GetAmmoData(out AmmoData ammoData)
    {
        ammoData = null;
        if (!_currentWeapon)
            return false;

        ammoData = _currentWeapon.GetAmmoData();
        return true;
    }

    public bool TryToAddAmmo(BaseWeapon weaponType, int clipsAmount)
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
    }
    protected bool CanFire()
    {
        return _currentWeapon && !_equipAnimInProgress && !_reloadAnimInProgress;
    }
    protected bool CanEquip()
    {
        return !_equipAnimInProgress && !_reloadAnimInProgress;
    }
    protected void EquipWeapon(int weaponIndex)
    {
        Debug.Log("lets equip");
        if (weaponIndex < 0 || weaponIndex >= _weapons.Count) return;
        Debug.Log("ok");
        if (_currentWeapon)
        {
            _currentWeapon.StopFire();
            AttachWeaponToSocket(_currentWeapon, _armoryEquipSocket);
        }
        _currentWeapon = _weapons[weaponIndex];
        Debug.Log(_currentWeapon.name);
        var currentWeaponData = _weaponDatas.FirstOrDefault((x) => x.Weapon == _currentWeapon.GetClass());

        _currentReloadAnim = currentWeaponData ? currentWeaponData.ReloadAnim : null;
        AttachWeaponToSocket(_currentWeapon, _weaponEquipSocket);
        //TODO WITH ANIM!
        //_equipAnimInProgress = true;
        PlayAnimMontage(_equipAnimation);
    }

    private void SpawnWeapons()
    {
        foreach (var weaponData in _weaponDatas)
        {
            var weapon = Instantiate(weaponData.Weapon, transform);
            if (!weapon) continue;
            weapon.Init();
            weapon.OnClipEmpty += OnEmptyClip;


            _weapons.Add(weapon);

            AttachWeaponToSocket(weapon, _armoryEquipSocket);
        }
    }
    private void AttachWeaponToSocket(BaseWeapon weapon, Transform socket)
    {
        if (!weapon) return;

        weapon.transform.SetParent(socket);
        weapon.transform.position = socket.position;
    }

    private void InitAnimations()
    {
        /*
          auto EquipFinishedNotify = AnimUtils::FindNotifyByClass<USTEquipFinishedAnimNotify>(EquipAnimMontage);
	if (EquipFinishedNotify)
	{
		EquipFinishedNotify->OnNotified.AddUObject(this, &USTWeaponComponent::OnEquipFinished);
	}
	else
	{
		UE_LOG(LogTemp, Error, TEXT("Equip anim notify is forgotten to set"));
	}

	for (auto WeaponData : WeaponsDatas)
	{
		auto ReloadFinishedNotify = AnimUtils::FindNotifyByClass<USTReloadFinishedAnimNotify>(WeaponData.ReloadAnimMontage);
		if (!ReloadFinishedNotify)
		{
			UE_LOG(LogTemp, Error, TEXT("Reload anim notify is forgotten to set"));
		}

		ReloadFinishedNotify->OnNotified.AddUObject(this, &USTWeaponComponent::OnRealoadFinished);
        
	}
        */
    }


    private void PlayAnimMontage(Animation animation)
    {

    }
    private void OnEquipFinished()
    {
        _equipAnimInProgress = false;
    }
  private  void OnRealoadFinished()
    {
        _reloadAnimInProgress = false;
    }

    private bool CanReload()
    {
        return _currentWeapon && !_equipAnimInProgress && !_reloadAnimInProgress && _currentWeapon.CanReload();
    }

    private void OnEmptyClip(BaseWeapon ammoEmpty)
    {
        if (!ammoEmpty)
            return;

        if (_currentWeapon == ammoEmpty)
            ChangeClip();
        else
        {
            foreach (var weapon in _weapons)
            {
                if (weapon != ammoEmpty)
                    continue;
                weapon.ChangeClip();
                break;
            }
        }
    }
    private void ChangeClip()
    {
        if (!CanReload()) return;

        _currentWeapon.StopFire();
        _currentWeapon.ChangeClip();
        _reloadAnimInProgress = true;
        PlayAnimMontage(_currentReloadAnim);
    }
  

}
