using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[System.Serializable]
public class HumanBone
{
    public HumanBodyBones Bone;
    public float Weight = 1.0f;
}
public class WeaponIK : MonoBehaviour
{

    [Range(0f, 1f)]
    [SerializeField] private float _weight = 1.0f;

    [SerializeField] private HumanBone[] _humanBones;
    private Transform[] _bonesTransforms;

    [SerializeField] private float _angleLimit = 90.0f;
    [SerializeField] private float _distanceLimit = 1.5f;

    private AIAnimationController _animationController;
    private AIWeaponOwner _weaponOwner;
    private Transform _target = null;
    private Transform _aim = null;

    [SerializeField] private MultiAimConstraint[] _aimingElements;
    [SerializeField] private RigBuilder _rigBuilder;
    private void Start()
    {
        _animationController = GetComponent<AIAnimationController>();
        _weaponOwner = GetComponent<AIWeaponOwner>();

        _bonesTransforms = new Transform[_humanBones.Length];
        for (int i = 0; i < _bonesTransforms.Length; i++)
        {
            _bonesTransforms[i] = _animationController.Animator.GetBoneTransform(_humanBones[i].Bone);
        }
    }
    private void LateUpdate()
    {
        if (_aim != null && _target != null)
        {

            //   for (int i = 0; i < _bonesTransforms.Length; i++)
            //  {
            //     AimTarget(_bonesTransforms[i], GetTargetPosition(), _humanBones[i].Weight * _weight);
            //  }
        }
    }
    public ThirdPlayerController GetTarget()
    {
        return _target?.GetComponent<ThirdPlayerController>();
    }
    public void SetTargetTransform(Transform target)
    {
        _target = target;

        if (_target == null)
        {
            Debug.Log("null");
            _animationController.RigAnimator.Play("reset_target", 0);
        }
        else
        {
            Debug.Log("not null");
            _animationController.RigAnimator.Play("equip_" + _weaponOwner.GetActiveWeapon().GunAnimatorName);
   //         _animationController.RigAnimator.SetInteger("weapon_index", (int)_weaponOwner.GetActiveWeapon().WeaponSlot);
        }


    }
    public void SetAimTransform(Transform aim)
    {
        _aim = aim;
    }
    private Vector3 GetTargetPosition()
    {
        Vector3 targetDirection = _target.position - _aim.position;
        Vector3 aimDirection = _aim.forward;
        float blendOut = 0.0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (targetAngle > _angleLimit)
        {
            blendOut += (targetAngle - _angleLimit) / 50.0f;
        }

        float targetDistance = targetDirection.magnitude;
        if (targetDistance > _distanceLimit)
        {
            blendOut += _distanceLimit - targetDistance;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return _aim.position + direction;
    }
    private void AimTarget(Transform bone, Vector3 targetPosition, float weight)
    {
        Vector3 aimDirection = _aim.forward;
        Vector3 targetDirection = targetPosition - _aim.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;
    }
}
