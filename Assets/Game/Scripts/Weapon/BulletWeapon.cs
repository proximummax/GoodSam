using System.Collections;
using UnityEditor;
using UnityEngine;

public class BulletWeapon : BaseWeapon
{

    [SerializeField] private ParticleSystem _muzzleFlashFX;

    [SerializeField] private float _bulletSpread = 1.5f;
    [SerializeField] private float _damageAmount = 10.0f;

    private Vector3 _target;
    public override void StartFire()
    {
        base.StartFire();
       

        //   MakeShot();
    }

    public override void StopFire()
    {
        base.StopFire();
        Reset();
    }

    protected override bool GetTraceData(ref Vector3 traceStart, ref Vector3 direction, Vector3 target)
    {
        Vector3 viewLocation = new Vector3();
        Quaternion viewRotation = new Quaternion();

        GetPlayerViewPoint(ref viewLocation, ref viewRotation);
        traceStart = viewLocation;

        direction = target - viewLocation;
        return true;
    }
  
    protected override void MakeShot(Vector3 target)
    {
        _target = target;
        if (IsAmmoEmpty())
        {
            StopFire();
            return;
        }

        Vector3 traceStart = new Vector3(), direction = new Vector3();
        if (!GetTraceData(ref traceStart, ref direction, target))
        {
            StopFire();
            return;
        }
        

        DecreaseAmmo();
        Vector3 velocity = direction * BulletSpeed;

        var bullet = CreateBullet(traceStart, velocity);
        Bullets.Add(bullet);

        _muzzleFlashFX.Emit(1);

        if (Recoil)
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
