using System.Collections;
using UnityEngine;

public class AIWeaponOwner : BaseWeaponOwnerComponent
{
    private WeaponIK _weaponIK;
    private bool _active = false;
    [SerializeField] private float _inaccuracy;
    [SerializeField] private BaseWeapon _weaponPrefab;

    protected override void Start()
    {
        base.Start();
        _weaponIK = GetComponent<WeaponIK>();

        BaseWeapon weapon = Instantiate(_weaponPrefab);
        EquipWeapon(weapon);
    }
    public override void EquipWeapon(BaseWeapon weapon)
    {
        base.EquipWeapon(weapon);
        weapon.SetFireRate(1f);
        weapon.SetRecoil(null);
        weapon.Init(_aimingComponent, _rigAnimator);
    }

    protected override void Update()
    {
        if (_weaponIK.GetTarget() && GetActiveWeapon() && _active)
        {
            Vector3 target = _weaponIK.GetTarget().EnemyTarget.position;
            target += Random.insideUnitSphere * _inaccuracy;
            if (GetActiveWeapon().IsFiring && !ReloadComponent.IsReloading)
                GetActiveWeapon().UpdateFiring(Time.deltaTime, target);

            GetActiveWeapon().UpdateBullets(Time.deltaTime);
        }

    }
    public void SetFiringState(bool enabled)
    {
        if (enabled)
        {
            GetActiveWeapon().StartFire();
        }
        else
        {
            GetActiveWeapon().StopFire();
        }
    }
    public void SetTarget(Transform target)
    {
        if (target)
            _weaponIK.SetTargetTransform(target);
    }
    public IEnumerator ActivateWeapon()
    {

        yield return new WaitForSeconds(0.5f);
        if (_weaponIK != null && GetActiveWeapon() != null)
            _weaponIK.SetAimTransform(GetActiveWeapon().MuzzleSocket);
        _active = true;
    }


    protected override void OnAmmoChanged(int ammo)
    {

    }
    protected override void OnReloadExit()
    {
        base.OnReloadExit();
        SetFiringState(true);
    }
}
