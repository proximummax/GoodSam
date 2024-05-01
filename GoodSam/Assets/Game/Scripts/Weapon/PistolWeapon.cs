
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PistolWeapon : BaseWeapon
{
    [SerializeField] private float _timeBetweenShots = 0.1f;
    [SerializeField] private float _bulletSpread = 1.5f;
    [SerializeField] private float _damageAmount = 10.0f;

    [SerializeField] private ParticleSystem _traceFX;
    [SerializeField] private ParticleSystem _muzzleFlashFX;
    [SerializeField] private GameObject _impactEffect;
    // [SerializeField] private Vector3 _bulletSpread;
    //FX

    public override void StartFire()
    {

        //    InitMuzzleFX();
        //  GetWorldTimerManager().SetTimer(ShotTimerHandle, this, &ASTRifleWeapon::MakeShot, TimeBetweenShots, true);
        MakeShot();
    }

    public override void StopFire()
    {
        //      GetWorldTimerManager().ClearTimer(ShotTimerHandle);
        //    SetMuzzleFXVisibility(false);
    }
    protected override bool GetTraceData(ref Vector3 traceStart, ref Vector3 direction)
    {
        Vector3 viewLocation = new Vector3();
        Quaternion viewRotation = new Quaternion();

        GetPlayerViewPoint(ref viewLocation, ref viewRotation);
        traceStart = viewLocation;

        direction = Random.insideUnitSphere * _bulletSpread + Camera.main.transform.forward;
        direction.Normalize();

        return true;
    }

    protected override void MakeShot()
    {
        if (IsAmmoEmpty())
        {
            Debug.Log("no ammo");
            StopFire();
            return;
        }

        Vector3 traceStart = new Vector3(), direction = new Vector3();
        if (!GetTraceData(ref traceStart, ref direction))
        {
            StopFire();
            return;
        }

        RaycastHit hitResult;
        MakeHit(out hitResult, traceStart, direction);

        Vector3 traceFXEnd = traceStart + direction * TraceMaxDistance;

        if (hitResult.collider != null)
        {
            Debug.Log(hitResult.collider.gameObject.name);
            if (hitResult.collider.TryGetComponent(out HitBox hitBox))
            {
                hitBox.ApplyHit(_damageAmount);
                // traceFXEnd = hitResult.ImpactPoint;
                MakeDamage(hitResult);
            }
            var impact = Instantiate(_impactEffect, hitResult.point, Quaternion.LookRotation(hitResult.normal));
            Destroy(impact, 2.0f);

            //  WeaponFXComponent->PlayImpactFX(hitResult);
        }
        // SpawnTraceFX(out ParticleSystem traceFX);
        //  StartCoroutine(MoveTraceFX(traceFX, MuzzleSocket.transform.position, traceFXEnd, 5.0f));
        DecreaseAmmo();
    }
    private void MakeDamage(RaycastHit hitResult)
    {


    }
    private void SpawnTraceFX(out ParticleSystem traceFX)
    {
        traceFX = Instantiate(_traceFX);
    }
    private IEnumerator MoveTraceFX(ParticleSystem traceFX, Vector3 traceStart, Vector3 traceEnd, float timeToReachEndPoint)
    {
        traceFX.transform.position = traceStart;
        traceFX.Play();

        float elapsedTime = 0.0f;
        Vector3 startingPosition = traceFX.transform.position;
        while (elapsedTime < timeToReachEndPoint)
        {
            traceFX.transform.position = Vector3.Lerp(traceStart, traceEnd, elapsedTime / timeToReachEndPoint);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        traceFX.transform.position = traceEnd;
        traceFX.Stop();

    }





}
