using UnityEngine;

public class BaseAnimationComponent : MonoBehaviour
{

    [SerializeField] private Animator _meshAnimator;
    public Animator MeshAnimator { get => _meshAnimator; }

    [SerializeField] private Animator _rigAnimator;
    public Animator RigAnimator { get => _rigAnimator; }

    public void Enable()
    {
        MeshAnimator.enabled = true;
    }
    public void Disable()
    {
        MeshAnimator.enabled = false;
    }
}
