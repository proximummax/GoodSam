using UnityEngine;

public class PlayerAnimationComponent : BaseAnimationComponent
{
    private readonly int INPUT_X = Animator.StringToHash("InputX");
    private readonly int INPUT_Y = Animator.StringToHash("InputY");
    private readonly int IS_JUMPING = Animator.StringToHash("isJumping");
    private readonly int IS_SPRINTING = Animator.StringToHash("isSprinting");
    private readonly int IS_AIMING = Animator.StringToHash("isAiming");

    private ThirdPlayerController _thirdPlayerController;

    [SerializeField] private Animator _rigController;

    private void Awake()
    {
        _thirdPlayerController = GetComponent<ThirdPlayerController>();
    }
   
    public void SetJumpingState(bool state)
    {
        MeshAnimator.SetBool(IS_JUMPING, state);
    }
    public void SetAimingState(bool state)
    {
        MeshAnimator.SetBool(IS_AIMING, state);
    }
    public void SetSprintState(bool state)
    {
        MeshAnimator.SetBool(IS_SPRINTING, state);
        _rigController.SetBool(IS_SPRINTING, state);
    }
    private void Update()
    {
        MeshAnimator.SetFloat(INPUT_X, _thirdPlayerController.Input.x);
        MeshAnimator.SetFloat(INPUT_Y, _thirdPlayerController.Input.y);
    }

  
}
