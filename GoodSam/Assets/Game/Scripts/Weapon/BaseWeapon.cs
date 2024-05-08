using UnityEngine;
using UnityEngine.Events;

public abstract class BaseWeapon : MonoBehaviour
{
    public UnityAction<BaseWeapon> OnClipEmpty;

    [Header("Weapon")]
    protected CrosshairTarget _crosshairTarget;
    [SerializeField] protected Transform MuzzleSocket;
    [SerializeField] protected WeaponData WeaponData;
    [SerializeField] protected AmmoData DefaultAmmo;
    [SerializeField] protected float TraceMaxDistance;

    [Header("UI")]
    [SerializeField] protected WeaponUIData UIData;

    [Header("Animation")]
    public AnimationClip WeaponAnimation;



    private AmmoData _currentAmmo;

    protected virtual void Awake()
    {
        enabled = false;

    }
    public void Init(CrosshairTarget crosshairTarget)
    {
        if (DefaultAmmo.Bullets <= 0)
            Debug.Log("BUllets count couldn`t be less of equal zero");
        if (DefaultAmmo.Clips <= 0)
            Debug.Log("Clips count couldn`t be less of equal zero");

        _crosshairTarget = crosshairTarget;
        _currentAmmo = DefaultAmmo;
    }
    public WeaponData GetData()
    {
        return WeaponData;
    }
   
    public abstract void StartFire();
    public abstract void StopFire();


    public void ChangeClip()
    {
        if (_currentAmmo.Infinite) return;
        if (_currentAmmo.Clips == 0) return;

        _currentAmmo.Clips--;
        _currentAmmo.Bullets = DefaultAmmo.Bullets;
    }
    public bool CanReload()
    {
        return _currentAmmo.Bullets < DefaultAmmo.Bullets && _currentAmmo.Clips > 0;
    }
    public bool IsAmmoEmpty()
    {
        return !_currentAmmo.Infinite && _currentAmmo.Clips == 0 && IsClipEmpty();
    }
    public bool IsAmmoFull()
    {
        return _currentAmmo.Clips == DefaultAmmo.Clips && _currentAmmo.Bullets == DefaultAmmo.Bullets;
    }
    protected bool IsClipEmpty()
    {
        return _currentAmmo.Bullets == 0;
    }

    public WeaponUIData GetUIData() { return UIData; }
    public AmmoData GetAmmoData() { return _currentAmmo; }

    public bool TryToAddAmmo(int clipsAmount)
    {
        if (IsAmmoFull() || _currentAmmo.Infinite || clipsAmount <= 0)
            return false;

        if (IsAmmoEmpty())
        {
            Debug.Log("Ammo was empty");

            _currentAmmo.Clips = Mathf.Clamp(_currentAmmo.Clips + clipsAmount, 0, DefaultAmmo.Clips + 1);
            OnClipEmpty?.Invoke(this);
        }
        else if (_currentAmmo.Clips < DefaultAmmo.Clips)
        {
            var nextClipsAmount = _currentAmmo.Clips + clipsAmount;
            if (DefaultAmmo.Clips - nextClipsAmount >= 0)
            {
                Debug.Log("Clips were added");
                _currentAmmo.Clips = nextClipsAmount;
            }
            else
            {
                Debug.Log("Ammo is full now");

                _currentAmmo.Clips = DefaultAmmo.Clips;
                _currentAmmo.Bullets = DefaultAmmo.Bullets;
            }
        }
        else
        {
            Debug.Log("Bullets were added");

            _currentAmmo.Bullets = DefaultAmmo.Bullets;
        }
        return true;
    }

    protected abstract void MakeShot();
    virtual protected bool GetTraceData(ref Vector3 traceStart, ref Vector3 direction)
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
        viewLocation = MuzzleSocket.transform.position;
        //viewLocation = Camera.main.transform.position;
        viewRotation = MuzzleSocket.transform.rotation;

    }

    protected void MakeHit(out RaycastHit hitResult, Vector3 traceStart, Vector3 direction)
    {
        Physics.Raycast(traceStart, direction, out hitResult, TraceMaxDistance);
    }
    protected void DecreaseAmmo()
    {
        if (_currentAmmo.Bullets == 0) return;

        _currentAmmo.Bullets--;

        if (IsClipEmpty() && !IsAmmoEmpty())
        {
            StopFire();
            OnClipEmpty?.Invoke(this);
        }
    }
}
