using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationComponent : MonoBehaviour
{
    private readonly int INPUT_X = Animator.StringToHash("InputX");
    private readonly int INPUT_Y = Animator.StringToHash("InputY");

    private Animator _animator;
    private ThirdPlayerController _thirdPlayerController;
    private void Awake()
    {
        _thirdPlayerController = GetComponent<ThirdPlayerController>();
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
    private void Update()
    {
        _animator.SetFloat(INPUT_X, _thirdPlayerController.Input.x);
        _animator.SetFloat(INPUT_Y, _thirdPlayerController.Input.y);
    }
}
