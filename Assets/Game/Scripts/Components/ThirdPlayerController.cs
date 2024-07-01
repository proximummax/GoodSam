using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private WeaponOwnerComponent _weaponOwner;
    private CharacterController _characterController;
    private AnimationComponent _animationComponent;

    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravity;
    [SerializeField] private float _stepDown;
    [SerializeField] private float _airControl;
    [SerializeField] private float _jumpDamp;
    [SerializeField] private float _groundSpeed;


    private Vector3 _velocity;
    public bool IsJumping { get; private set; }

    public CharacterController CharacterController { get { return _characterController; } }
    public Vector2 Input { get; private set; }
    public bool IsAiming { get; private set; }

    private Vector3 _rootMotion;
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animationComponent = GetComponent<AnimationComponent>();
    }
    private void OnEnable()
    {
        _playerInput.Enable();

        _weaponOwner = GetComponent<WeaponOwnerComponent>();

        _playerInput.Player.SelectWeapon_1.performed += _weaponOwner.SelectWeapon;
        _playerInput.Player.SelectWeapon_2.performed += _weaponOwner.SelectWeapon;


        _playerInput.Player.Shoot.started += _weaponOwner.StartFire;
        _playerInput.Player.Shoot.canceled += _weaponOwner.StopFire;

        _playerInput.Player.Holster.performed += _weaponOwner.Holster;
        _playerInput.Player.Reload.performed += _weaponOwner.Reload;

        _playerInput.Player.Jump.performed += Jump;
      //    _playerInput.Player.Jump.canceled += Jump;

    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
    private void Update()
    {
        Input = _playerInput.Player.Move.ReadValue<Vector2>();
        IsAiming = _playerInput.Player.Zoom.phase == InputActionPhase.Performed;
    }
  
    private void OnAnimatorMove()
    {
        _rootMotion += _animationComponent.Animator.deltaPosition;
    }
    private void FixedUpdate()
    {
        if (IsJumping)
        {
            UpdateInAir();
        }
        else
        {
            UpdateOnGround();
        }
    }

    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = _rootMotion * _groundSpeed;
        Vector3 stepDownAmount = Vector3.down * _stepDown;

        CharacterController.Move(stepForwardAmount + stepDownAmount);
        _rootMotion = Vector3.zero;

        if (!CharacterController.isGrounded)
        {
            SetInAir(0);
          
        }
    }

    private void UpdateInAir()
    {
        _velocity.y -= _gravity * Time.fixedDeltaTime;

        Vector3 displacement = _velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        CharacterController.Move(displacement);

        IsJumping = !CharacterController.isGrounded;
        _rootMotion = Vector3.zero;

        _animationComponent.SetJumpingState(IsJumping);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!IsJumping)
        {
            SetInAir(Mathf.Sqrt(2 * _gravity * _jumpHeight));
        }
    }
    private void SetInAir(float jumpVelocity)
    {
        IsJumping = true;
        _velocity = _animationComponent.Animator.velocity * _jumpDamp * _groundSpeed;
        _velocity.y = jumpVelocity;
        _animationComponent.SetJumpingState(true);
    }
    private Vector3 CalculateAirControl()
    {
        return ((transform.forward * Input.y) + (transform.right * Input.x)) * (_airControl/100);
    }
}
