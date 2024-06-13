using System.Collections;
using UnityEngine;

public class PistolWeapon : BaseWeapon
{
    [SerializeField] private float _bulletSpread = 1.5f;
    [SerializeField] private float _damageAmount = 10.0f;

    [SerializeField] private ParticleSystem _muzzleFlashFX;
   


    public override void StartFire()
    {
        base.StartFire();
        _muzzleFlashFX.Emit(1);

        MakeShot();
      
    }

    public override void StopFire()
    {
        base.StopFire();
        Reset();
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
            Debug.Log("STOP");
            return;
        }

        Vector3 traceStart = new Vector3(), direction = new Vector3();
        if (!GetTraceData(ref traceStart, ref direction))
        {
            StopFire();
            return;
        }

        DecreaseAmmo();
        Vector3 velocity = direction * BulletSpeed;
        var bullet = CreateBullet(traceStart, velocity);
        Bullets.Add(bullet);


        Recoil.GenerateRecoil(GunAnimatorName);
      
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
