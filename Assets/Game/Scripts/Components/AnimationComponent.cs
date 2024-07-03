using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationComponent : MonoBehaviour
{
    private readonly int INPUT_X = Animator.StringToHash("InputX");
    private readonly int INPUT_Y = Animator.StringToHash("InputY");
    private readonly int IS_JUMPING = Animator.StringToHash("isJumping");
    private readonly int IS_SPRINTING = Animator.StringToHash("isSprinting");
    private readonly int IS_AIMING = Animator.StringToHash("isAiming");
   public Animator Animator { get; private set; }
    private ThirdPlayerController _thirdPlayerController;

    [SerializeField] private Animator _rigController;

    private void Awake()
    {
        _thirdPlayerController = GetComponent<ThirdPlayerController>();
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
    public void SetJumpingState(bool state)
    {
        Animator.SetBool(IS_JUMPING, state);
    }
    public void SetAimingState(bool state)
    {
        Debug.Log("aiming is " + state);
        Animator.SetBool(IS_AIMING, state);
    }
    public void SetSprintState(bool state)
    {
        Animator.SetBool(IS_SPRINTING, state);
        _rigController.SetBool(IS_SPRINTING, state);
    }
    private void Update()
    {
        Animator.SetFloat(INPUT_X, _thirdPlayerController.Input.x);
        Animator.SetFloat(INPUT_Y, _thirdPlayerController.Input.y);
    }

  
}
