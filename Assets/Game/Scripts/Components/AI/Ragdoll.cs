using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;
    private AIAnimationController _animationComponent;
    private void Start()
    {
        _animationComponent = GetComponent<AIAnimationController>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        DeactivateRagdoll();
    }
    public void DeactivateRagdoll()
    {
        foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = true;
        _animationComponent.Enable();
    }
    public void ActivateRagdoll()
    {
        foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = false;
        _animationComponent.Disable();
    }

}
