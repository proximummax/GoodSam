using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;


public class WeaponOwnerComponent : MonoBehaviour
{
    [SerializeField] private CrosshairTarget _crosshairTarget;
    [SerializeField] private List<BaseWeapon> _weaponDatas;
    [SerializeField] private Transform _weaponEquipSocket;
    [SerializeField] private Transform _armoryEquipSocket;
    [Header("Animations")]
    [SerializeField] private Transform _leftGrip;
    [SerializeField] private Transform _rightGrip;
    [SerializeField] private Rig _handIK;

    [SerializeField] private BaseWeapon _currentWeapon = null;

    private Animator _animator;
    private AnimatorOverrideController _overrides;
    //   private List<BaseWeapon> _weapons = new List<BaseWeapon>();
    //  private int _currentWeaponIndex = 0;

    private Animation _currentReloadAnim = null;
    private bool _equipAnimInProgress = false;
    private bool _reloadAnimInProgress = false;

    private bool _firstEquip = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _overrides = _animator.runtimeAnimatorController as AnimatorOverrideController;

        _handIK.weight = 0.0f;
        _animator.SetLayerWeight(1, 0.0f);

        InitAnimations();
        //    _currentWeaponIndex = 0;
        //   SpawnWeapons();
        EquipWeapon(_currentWeapon);
    }
    public void StartFire(InputAction.CallbackContext context)
    {
        Debug.Log("call?");
        if (!CanFire()) return;
        Debug.Log("can?");
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

        //      _currentWeaponIndex = (_currentWeaponIndex + 1) % _weaponDatas.Count;
        //   EquipWeapon(_currentWeaponIndex);
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
        return _currentWeapon && !_equipAnimInProgress && !_reloadAnimInProgress;
    }
    protected bool CanEquip()
    {
        return !_equipAnimInProgress && !_reloadAnimInProgress;
    }
    public void EquipWeapon(BaseWeapon weapon)
    {
        //  if (weaponIndex < 0 || weaponIndex >= _weapons.Count) return;
        if (weapon == null) return;

        if (_currentWeapon && _firstEquip == false)
        {
            _currentWeapon.StopFire();
            Destroy(_currentWeapon.gameObject);
            //       AttachWeaponToSocket(_currentWeapon, _armoryEquipSocket);
        }
        _firstEquip = false;
        _currentWeapon = weapon;

        AttachWeaponToSocket(_currentWeapon, _weaponEquipSocket);
        _currentWeapon.Init(_crosshairTarget);
        _currentWeapon.OnClipEmpty += OnEmptyClip;

        //    var currentWeaponData = _weaponDatas.FirstOrDefault((x) => x.GetData().Weapon == _currentWeapon.GetData().Weapon);

        //    _currentReloadAnim = currentWeaponData ? currentWeaponData.GetData().ReloadAnim : null;

        //  AttachWeaponToSocket(_currentWeapon, _weaponEquipSocket);

        _handIK.weight = 1.0f;
        _animator.SetLayerWeight(1, 1.0f);
        Invoke(nameof(SetAnimationDelayed), 0.001f);
       
    }

    private void SetAnimationDelayed()
    {
        _overrides["weapon_anim_empty"] = _currentWeapon.WeaponAnimation;
    }
    private void SpawnWeapons()
    {
        foreach (var weapon in _weaponDatas)
        {
            //var weapon = Instantiate(weaponData.Weapon, transform);
            if (!weapon) continue;

            //     weapon.Init(_crosshairTarget);
            //   weapon.OnClipEmpty += OnEmptyClip;


            //      _weapons.Add(weapon);

            //       AttachWeaponToSocket(weapon, _armoryEquipSocket);
        }
    }
    private void AttachWeaponToSocket(BaseWeapon weapon, Transform socket)
    {
        if (!weapon) return;

        weapon.transform.SetParent(socket);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
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
    private void OnRealoadFinished()
    {
        _reloadAnimInProgress = false;
    }

    private bool CanReload()
    {
        return _currentWeapon && !_equipAnimInProgress && !_reloadAnimInProgress && _currentWeapon.CanReload();
    }

    private void OnEmptyClip(BaseWeapon ammoEmpty)
    {
        /*      if (!ammoEmpty)
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
              }*/
    }
    private void ChangeClip()
    {
        if (!CanReload()) return;

        _currentWeapon.StopFire();
        _currentWeapon.ChangeClip();
        _reloadAnimInProgress = true;
        PlayAnimMontage(_currentReloadAnim);
    }
    [ContextMenu("Save weapon pose")]
    private void SaveWeaponPose()
    {
        GameObjectRecorder gameObjectRecorder = new GameObjectRecorder(gameObject);
        gameObjectRecorder.BindComponentsOfType<Transform>(_weaponEquipSocket.gameObject, false);
        gameObjectRecorder.BindComponentsOfType<Transform>(_leftGrip.gameObject, false);
        gameObjectRecorder.BindComponentsOfType<Transform>(_rightGrip.gameObject, false);
        gameObjectRecorder.TakeSnapshot(0.0f);
        gameObjectRecorder.SaveToClip(_currentWeapon.WeaponAnimation);
        UnityEditor.AssetDatabase.SaveAssets();
    }

}
