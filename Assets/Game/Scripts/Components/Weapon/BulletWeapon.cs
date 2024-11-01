using UnityEngine;

public class BulletWeapon : BaseWeapon
{
    [SerializeField] private ParticleSystem _muzzleFlashFX;

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

        if (MusicManager.MusicVolume > 0.0f)
            _source.PlayOneShot(_fireClip);

        DecreaseAmmo();
        Vector3 velocity = direction * BulletSpeed;

        var bullet = CreateBullet(traceStart, velocity);
        Bullets.Add(bullet);

        _muzzleFlashFX.Emit(1);

        if (Recoil)
            Recoil.GenerateRecoil(GunAnimatorName);

    }
    
}
