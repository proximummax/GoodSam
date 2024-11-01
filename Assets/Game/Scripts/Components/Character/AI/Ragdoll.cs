using UnityEngine;

[RequireComponent(typeof(BaseAnimationComponent))]
public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Transform _meshRoot;
    private Rigidbody[] _rigidbodies;
    private BaseAnimationComponent _animationComponent;

    private void Start()
    {
        _animationComponent = GetComponent<BaseAnimationComponent>();
        _rigidbodies = _meshRoot.GetComponentsInChildren<Rigidbody>();
        DeactivateRagdoll();
    }

    private void DeactivateRagdoll()
    {
        foreach (var rbody in _rigidbodies)
            rbody.isKinematic = true;
        _animationComponent.Enable();
    }

    public void ActivateRagdoll()
    {
        foreach (var rbody in _rigidbodies)
            rbody.isKinematic = false;
        _animationComponent.Disable();
    }
}