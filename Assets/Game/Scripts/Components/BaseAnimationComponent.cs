using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseAnimationComponent : MonoBehaviour
{

    public Animator Animator { get; private set; }
    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    public void Enable()
    {
        Animator.enabled = true;
    }
    public void Disable()
    {
        Animator.enabled = false;
    }
}
