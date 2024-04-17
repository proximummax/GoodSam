using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public abstract class BaseWeapon : MonoBehaviour
{
    public UnityAction<BaseWeapon> OnClipEmpty;

    [Header("Weapon")]
    [SerializeField] protected Transform _muzzleSocket;
    [SerializeField] protected WeaponData _weaponData;
    [SerializeField] protected AmmoData _defaultAmmo;
    [SerializeField] protected float _traceMaxDistance;

    [Header("UI")]
    [SerializeField] protected WeaponUIData _uiData;

    // [Header("VFX")]
    //TODO: vfx



    private AmmoData _currentAmmo;

    protected virtual void Awake()
    {
        enabled = false;

    }
    protected virtual void Start()
    {
        if (_defaultAmmo.Bullets <= 0)
            Debug.Log("BUllets count couldn`t be less of equal zero");
        if (_defaultAmmo.Clips <= 0)
            Debug.Log("Clips count couldn`t be less of equal zero");

        _currentAmmo = _defaultAmmo;
    }


    public abstract void StartFire();
    public abstract void StopFire();


    public void ChangeClip()
    {
        if (_currentAmmo.Infinite) return;
        if (_currentAmmo.Clips == 0) return;

        _currentAmmo.Clips--;
        _currentAmmo.Bullets = _defaultAmmo.Bullets;
    }
    public bool CanReload()
    {
        return _currentAmmo.Bullets < _defaultAmmo.Bullets && _currentAmmo.Clips > 0;
    }
    public bool IsAmmoEmpty()
    {
        return !_currentAmmo.Infinite && _currentAmmo.Clips == 0 && IsClipEmpty();
    }
    public bool IsAmmoFull()
    {
        return _currentAmmo.Clips == _defaultAmmo.Clips && _currentAmmo.Bullets == _defaultAmmo.Bullets;
    }
    protected bool IsClipEmpty()
    {
        return _currentAmmo.Bullets == 0;
    }

    public WeaponUIData GetUIData() { return _uiData; }
    public AmmoData GetAmmoData() { return _currentAmmo; }
    public bool TryToAddAmmo(int clipsAmount)
    {
        if (IsAmmoFull() || _currentAmmo.Infinite || clipsAmount <= 0)
            return false;

        if (IsAmmoEmpty())
        {
            Debug.Log("Ammo was empty");

            _currentAmmo.Clips = Mathf.Clamp(_currentAmmo.Clips + clipsAmount, 0, _defaultAmmo.Clips + 1);
            OnClipEmpty?.Invoke(this);
        }
        else if (_currentAmmo.Clips < _defaultAmmo.Clips)
        {
            var nextClipsAmount = _currentAmmo.Clips + clipsAmount;
            if (_defaultAmmo.Clips - nextClipsAmount >= 0)
            {
                Debug.Log("Clips were added");
                _currentAmmo.Clips = nextClipsAmount;
            }
            else
            {
                Debug.Log("Ammo is full now");

                _currentAmmo.Clips = _defaultAmmo.Clips;
                _currentAmmo.Bullets = _defaultAmmo.Bullets;
            }
        }
        else
        {
            Debug.Log("Bullets were added");

            _currentAmmo.Bullets = _defaultAmmo.Bullets;
        }
        return true;
    }

    protected abstract void MakeShot();
    protected bool GetTraceData(ref Vector3 traceStart, ref Vector3 traceEnd)
    {
        Vector3 viewLocation = new Vector3();
        Quaternion viewRotation = new Quaternion();

        GetPlayerViewPoint(ref viewLocation, ref viewRotation);
        traceStart = viewLocation;

        Vector3 shootDirection = viewRotation.eulerAngles;
        traceEnd = traceStart + shootDirection * _traceMaxDistance;

        return true;
    }
    protected void GetPlayerViewPoint(ref Vector3 viewLocation, ref Quaternion viewRotation)
    {
        viewLocation = _muzzleSocket.transform.position;
        viewRotation = _muzzleSocket.transform.rotation;

    }

    protected void MakeHit(RaycastHit hitResult, Vector3 traceStart, Vector3 traceEnd)
    {
       Physics.Raycast(traceStart, traceEnd);
       //????
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
