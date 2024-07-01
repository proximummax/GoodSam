using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationComponent : MonoBehaviour
{
    private readonly int INPUT_X = Animator.StringToHash("InputX");
    private readonly int INPUT_Y = Animator.StringToHash("InputY");
    private readonly int IS_JUMPING = Animator.StringToHash("isJumping");
   public Animator Animator { get; private set; }
    private ThirdPlayerController _thirdPlayerController;


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
    private void Update()
    {
        Animator.SetFloat(INPUT_X, _thirdPlayerController.Input.x);
        Animator.SetFloat(INPUT_Y, _thirdPlayerController.Input.y);
        Debug.Log(_thirdPlayerController.Input);
    }

  
}
