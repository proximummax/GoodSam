using UnityEngine;

public class BaseAnimationComponent : MonoBehaviour
{
    [SerializeField] private Animator _meshAnimator;
    public Animator MeshAnimator => _meshAnimator;
    
    public void Enable()
    {
        MeshAnimator.enabled = true;
    }
    public void Disable()
    {
        MeshAnimator.enabled = false;
    }
}
