using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseWeapon : MonoBehaviour
{
    public UnityAction<BaseWeapon> OnClipEmpty;
    public UnityAction<int> OnAmmoChanged;
    public UnityAction OnReloaded;

    [Header("Weapon")] 

    [SerializeField] protected BaseWeaponOwnerComponent.EWeaponSlot _weaponSlot;
    [SerializeField] private Transform _muzzleSocket;

    [SerializeField] protected AmmoData DefaultAmmo;
    [SerializeField] protected float TraceMaxDistance;

    [Header("Animation")] [SerializeField] string _gunAnimatorName;

    [Header("Bullet settings")] 
    [SerializeField] protected float FireRate = 25;

    [SerializeField] protected float BulletSpeed = 1000;
    [SerializeField] protected float BulletDrop = 0.0f;
    [SerializeField] protected float MaxLifeTime = 3.0f;
    [SerializeField] protected float DamageAmount = 10.0f;

    protected List<BaseBullet> Bullets = new List<BaseBullet>();
    
    [Header("FX")] 
    [SerializeField] protected TrailRenderer TraceFX;
    [SerializeField] protected ParticleSystem HitEffect;

    [Header("Sounds")] 
    [SerializeField] protected AudioSource _source;
    [SerializeField] protected AudioClip _fireClip;
    [SerializeField] protected AudioClip _reloadClip;

    private AmmoData _currentAmmo;
    private float _accumulatedTime = 0.0f;
    
    public BaseRecoil Recoil { get; private set; }

    public bool IsFiring { get; private set; }
    public string GunAnimatorName => _gunAnimatorName;
    public BaseWeaponOwnerComponent.EWeaponSlot WeaponSlot => _weaponSlot;

    protected virtual void Awake()
    {
        enabled = false;
        Recoil = GetComponent<BaseRecoil>();
    }

    public void SetFireRate(float rate)
    {
        FireRate = rate;
    }

    public void SetRecoil(BaseRecoil recoil)
    {
        Recoil = recoil;
    }

    protected virtual BaseBullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        BaseBullet bullet = new BaseBullet
        {
            InitialPosition = position,
            InitialVelocity = velocity,
            Time = 0.0f,
            Tracer = Instantiate(TraceFX, position, Quaternion.identity)
        };
        bullet.Tracer.AddPosition(position);
        return bullet;
    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    private void SimulateBullets(float deltaTime)
    {
        Bullets.ForEach(bullet =>
        {
            Vector3 p0 = bullet.GetPosition(Vector3.down * BulletDrop);
            bullet.Time += deltaTime;
            Vector3 p1 = bullet.GetPosition(Vector3.down * BulletDrop);
            RaycastSegment(p0, p1, bullet);
        });
    }

    private void DestroyBullets()
    {
        Bullets.RemoveAll(bullet => bullet.Time >= MaxLifeTime);
    }

    private void RaycastSegment(Vector3 start, Vector3 end, BaseBullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        MakeHit(out var hitResult, start, direction, distance);

        if (hitResult.collider != null)
        {
            if (hitResult.collider.TryGetComponent(out HitBoxComponent hitBox))
            {
                hitBox.ApplyHit(DamageAmount);
            }
            else
            {
                HitEffect.transform.position = hitResult.point;
                HitEffect.transform.forward = hitResult.normal;
                HitEffect.Emit(1);
            }

            if (bullet.Tracer)
                bullet.Tracer.transform.position = hitResult.point;
            bullet.Time = MaxLifeTime;
        }
        else if (bullet.Tracer)
        {
            bullet.Tracer.transform.position = end;
        }
    }

    public void UpdateFiring(float deltaTime, Vector3 target)
    {
        _accumulatedTime += deltaTime;
        float fireInterval = 1.0f / FireRate;
        while (_accumulatedTime >= 0.0f)
        {
            MakeShot(target);
            _accumulatedTime -= fireInterval;
        }
    }

    public void Init(AimingComponent aimingComponent, Animator rigController)
    {
        if (DefaultAmmo.Bullets <= 0)
            Debug.Log("BUllets count couldn`t be less of equal zero");

        if (Recoil)
        {
            Recoil.Inititalize(aimingComponent, rigController);
        }

        _currentAmmo = ScriptableObject.CreateInstance<AmmoData>();
        _currentAmmo.Init(DefaultAmmo);

        OnAmmoChanged?.Invoke(_currentAmmo.Bullets);
    }

    protected void Reset()
    {
        if (Recoil)
            Recoil.Reset();
    }

    public virtual void StartFire()
    {
        if (IsFiring)
            return;
        IsFiring = true;
    }

    public virtual void StopFire()
    {
        if (!IsFiring)
            return;
        IsFiring = false;
    }

    public void ChangeClip()
    {
        _currentAmmo.Bullets = DefaultAmmo.Bullets;

        OnAmmoChanged?.Invoke(_currentAmmo.Bullets);
        OnReloaded?.Invoke();

        if (MusicManager.MusicVolume > 0.0f)
            _source.PlayOneShot(_reloadClip);
    }

    public bool CanReload()
    {
        return _currentAmmo.Bullets < DefaultAmmo.Bullets;
    }

    public bool IsAmmoEmpty()
    {
        return IsClipEmpty();
    }

    private bool IsAmmoFull()
    {
        return _currentAmmo.Bullets == DefaultAmmo.Bullets;
    }

    private bool IsClipEmpty()
    {
        return _currentAmmo.Bullets == 0;
    }

    public AmmoData GetAmmoData()
    {
        return _currentAmmo;
    }

    public bool TryToAddAmmo(int clipsAmount)
    {
        if (IsAmmoFull() || clipsAmount <= 0)
            return false;

        if (IsAmmoEmpty())
        {
            OnClipEmpty?.Invoke(this);
        }
        else
        {
            _currentAmmo.Bullets = DefaultAmmo.Bullets;
        }

        OnAmmoChanged?.Invoke(_currentAmmo.Bullets);

        return true;
    }

    protected abstract void MakeShot(Vector3 target);

    protected virtual bool GetTraceData(ref Vector3 traceStart, ref Vector3 direction, Vector3 target)
    {
        Vector3 viewLocation = new Vector3();
        Quaternion viewRotation = new Quaternion();

        GetPlayerViewPoint(ref viewLocation, ref viewRotation);
        traceStart = viewLocation;


        Vector3 shootDirection = viewRotation.eulerAngles;
        direction = traceStart + shootDirection * TraceMaxDistance;

        return true;
    }

    protected void GetPlayerViewPoint(ref Vector3 viewLocation, ref Quaternion viewRotation)
    {
        viewLocation = _muzzleSocket.transform.position;
        viewRotation = _muzzleSocket.transform.rotation;
    }

    private void MakeHit(out RaycastHit hitResult, Vector3 traceStart, Vector3 direction, float distance)
    {
        Physics.Raycast(traceStart, direction, out hitResult, distance);
    }

    protected void DecreaseAmmo()
    {
        if (_currentAmmo.Bullets == 0) return;
        _currentAmmo.Bullets--;

        OnAmmoChanged?.Invoke(_currentAmmo.Bullets);

        if (IsClipEmpty())
        {
            StopFire();
            OnClipEmpty?.Invoke(this);
        }
    }
}