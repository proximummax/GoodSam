using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationComponent : MonoBehaviour
{
    private Animator _animator;
    private readonly int RUN_STATE = Animator.StringToHash("Run");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Enable()
    {
        _animator.enabled = true;
    }
    public void Disable()
    {
        _animator.enabled = false;
    }
    public void SetRunState(bool state)
    {
        _animator.SetBool(RUN_STATE, state);
    }
}
