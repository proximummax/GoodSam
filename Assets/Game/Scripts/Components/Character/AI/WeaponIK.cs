using UnityEngine;

public class WeaponIK : MonoBehaviour
{
    private Transform _target = null;
    
    public ThirdPlayerController GetTarget()
    {
        return _target?.GetComponent<ThirdPlayerController>();
    }
    public void SetTargetTransform(Transform target)
    {
        _target = target;
    }
}
