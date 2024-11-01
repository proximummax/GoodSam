using UnityEngine;

public class AIAnimationController : BaseAnimationComponent
{
    private readonly int SPEED = Animator.StringToHash("Speed");

    [SerializeField]
    private AIAgent _agent;

    public void SetSpeed(float speed)
    {
        MeshAnimator.SetFloat(SPEED, speed);
    }
   
}
