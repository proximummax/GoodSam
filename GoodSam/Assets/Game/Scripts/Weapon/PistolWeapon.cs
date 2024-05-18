using System.Collections;
using UnityEngine;

public class PistolWeapon : BaseWeapon
{
    [SerializeField] private float _bulletSpread = 1.5f;
    [SerializeField] private float _damageAmount = 10.0f;

    [SerializeField] private TrailRenderer _traceFX;
    [SerializeField] private ParticleSystem _muzzleFlashFX;
    [SerializeField] private ParticleSystem _hitEffect;

    float _elapsedTime = 0.0f;
    public override void StartFire()
    {
        _elapsedTime = 0.0f;
        _muzzleFlashFX.Emit(1);
        
        MakeShot();
        Recoil.GenerateRecoil(GunAnimatorName);
    }

   
    public override void StopFire()
    {

    }

    protected override bool GetTraceData(ref Vector3 traceStart, ref Vector3 direction)
    {
        Vector3 viewLocation = new Vector3();
        Quaternion viewRotation = new Quaternion();

        GetPlayerViewPoint(ref viewLocation, ref viewRotation);
        traceStart = viewLocation;

        direction = _crosshairTarget.transform.position - viewLocation;
     //   direction = Random.insideUnitSphere * _bulletSpread + Camera.main.transform.forward;
     //   direction.Normalize();

        return true;
    }

    protected override void MakeShot()
    {
        if (IsAmmoEmpty())
        {
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
        
        var tracer = Instantiate(_traceFX, MuzzleSocket.transform.position, Quaternion.identity);
        tracer.AddPosition(MuzzleSocket.transform.position);

        if (hitResult.collider != null)
        {
            if (hitResult.collider.TryGetComponent(out HitBox hitBox))
            {
                hitBox.ApplyHit(_damageAmount);

                MakeDamage(hitResult);

               
            }
            tracer.transform.position = hitResult.point;
            _hitEffect.transform.position = hitResult.point;
            _hitEffect.transform.forward = hitResult.normal;
            _hitEffect.Emit(1);
     
        }
        DecreaseAmmo();
    }


    private void MakeDamage(RaycastHit hitResult)
    {


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
