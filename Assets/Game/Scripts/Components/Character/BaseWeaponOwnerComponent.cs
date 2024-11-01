using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;


public abstract class BaseWeaponOwnerComponent : MonoBehaviour
{
    private readonly int WEAPON_INDEX = Animator.StringToHash("weapon_index");
    protected readonly int HOLSTER_WEAPON = Animator.StringToHash("holster_weapon");

    public enum EWeaponSlot
    {
        Primary,
        Secondary,
        Count
    }

    [Header("Animations")] 
    [SerializeField] private Transform _leftGrip;
    [SerializeField] private Transform _rightGrip;
    [SerializeField] private Rig _handIK;
    [SerializeField] protected Animator _rigAnimator;
    [SerializeField] protected AimingComponent _aimingComponent;
    
    [Header("Sub Components")]
    [SerializeField] private CrosshairTarget _crosshairTarget;
    [SerializeField] private Transform[] _weaponSlots;
    [SerializeField] private ReloadComponent _reloadComponent;

    private BaseWeapon[] _equipedWeapons = new BaseWeapon[(int)EWeaponSlot.Count];
    private bool _isHolstered = false;
    
    protected int CurrentWeaponIndex = 0;
    
    public ReloadComponent ReloadComponent => _reloadComponent;
    public bool IsWeaponChangeInProcess { get; private set; } = false;


    protected virtual void Start()
    {
        InitAnimations();
    }

    protected virtual void Update()
    {
        var weapon = GetWeapon(CurrentWeaponIndex);
        bool notSprinting = _rigAnimator.GetCurrentAnimatorStateInfo(2).shortNameHash ==
                            Animator.StringToHash("notSprinting");
        
        if (!weapon || _isHolstered || !notSprinting) return;
        
        if (weapon.IsFiring)
            weapon.UpdateFiring(Time.deltaTime, _crosshairTarget.transform.position);

        weapon.UpdateBullets(Time.deltaTime);
    }

    private BaseWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= _equipedWeapons.Length)
            return null;
        return _equipedWeapons[index];
    }

    private BaseWeapon[] GetWeapons()
    {
        return _equipedWeapons;
    }

    public BaseWeapon GetActiveWeapon()
    {
        return GetWeapon(CurrentWeaponIndex);
    }

    private void ResetWeapon()
    {
        CurrentWeaponIndex = -1;
    }

    public void StartFire(InputAction.CallbackContext context = default)
    {
        if (!CanFire()) return;
        GetWeapon(CurrentWeaponIndex).StartFire();
    }

    public void StopFire(InputAction.CallbackContext context = default)
    {
        if (!GetWeapon(CurrentWeaponIndex)) return;
        GetWeapon(CurrentWeaponIndex).StopFire();
    }

    public void SelectWeapon(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "SelectWeapon_1":
                StartCoroutine(SwitchWeapon(CurrentWeaponIndex, EWeaponSlot.Primary));
                break;
            case "SelectWeapon_2":
                StartCoroutine(SwitchWeapon(CurrentWeaponIndex, EWeaponSlot.Secondary));
                break;
        }
    }
    
    public void Reload(InputAction.CallbackContext context = default)
    {
        if (_reloadComponent)
            _reloadComponent.SetReloadTrigger();
        ChangeClip();
    }
    
    public void TryToAddAmmo(int clipsAmount)
    {
        GetActiveWeapon().TryToAddAmmo(clipsAmount);
    }

    private bool CanFire()
    {
        return GetWeapon(CurrentWeaponIndex) && !_isHolstered;
    }
    
    public virtual void EquipWeapon(BaseWeapon weapon)
    {
        if (weapon == null) return;

        if (GetWeapon(CurrentWeaponIndex))
        {
            GetWeapon(CurrentWeaponIndex).StopFire();
        }

        int weaponIndex = (int)weapon.WeaponSlot;
        _equipedWeapons[weaponIndex] = weapon;


        AttachWeaponToSocket(weapon, _weaponSlots[(int)weapon.WeaponSlot]);

        StartCoroutine(SwitchWeapon(CurrentWeaponIndex, (EWeaponSlot)weaponIndex));

        weapon.OnClipEmpty += OnEmptyClip;
        weapon.OnAmmoChanged += OnAmmoChanged;
        weapon.OnReloaded += OnReloadExit;
    }


    protected virtual IEnumerator SwitchWeapon(int holsterIndex, EWeaponSlot activeSlot)
    {
        if (holsterIndex == (int)activeSlot)
            holsterIndex = -1;

        _rigAnimator.SetInteger(WEAPON_INDEX, (int)activeSlot);
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon((int)activeSlot));
        CurrentWeaponIndex = (int)activeSlot;

        if (GetActiveWeapon())
            OnAmmoChanged(GetActiveWeapon().GetAmmoData().Bullets);
    }

    protected IEnumerator HolsterWeapon(int weaponIndex)
    {
        IsWeaponChangeInProcess = true;
        _isHolstered = true;
        var weapon = GetWeapon(weaponIndex);
        if (weapon)
        {
            _rigAnimator.SetBool(HOLSTER_WEAPON, true);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (_rigAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }

        IsWeaponChangeInProcess = false;
    }

    protected IEnumerator ActivateWeapon(int weaponIndex)
    {
        IsWeaponChangeInProcess = true;
        var weapon = GetWeapon(weaponIndex);
        if (weapon)
        {
            _rigAnimator.SetBool(HOLSTER_WEAPON, false);
            _rigAnimator.Play("equip_" + weapon.GunAnimatorName);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (_rigAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }

        _isHolstered = false;
        IsWeaponChangeInProcess = false;
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
        return GetWeapon(CurrentWeaponIndex) && GetWeapon(CurrentWeaponIndex).CanReload();
    }

    private void OnEmptyClip(BaseWeapon ammoEmpty)
    {
        if (!ammoEmpty)
            return;

        if (GetActiveWeapon() == ammoEmpty)
            Reload();
    }

    private void ChangeClip()
    {
        if (!CanReload()) return;

        GetWeapon(CurrentWeaponIndex).StopFire();
        GetWeapon(CurrentWeaponIndex).ChangeClip();
    }

    public void DropWeapon()
    {
        foreach (var weapon in GetWeapons())
        {
            if (weapon != null)
            {
                weapon.transform.SetParent(null);
                weapon.gameObject.GetComponent<BoxCollider>().enabled = true;
                weapon.gameObject.AddComponent<Rigidbody>();
                ResetWeapon();
            }
        }
    }

    protected abstract void OnAmmoChanged(int ammo);

    protected virtual void OnReloadExit()
    {
    }
}