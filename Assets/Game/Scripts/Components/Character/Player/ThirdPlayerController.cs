using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravity;
    [SerializeField] private float _stepDown;
    [SerializeField] private float _airControl;
    [SerializeField] private float _jumpDamp;
    [SerializeField] private float _groundSpeed;

    [SerializeField] private Transform _enemyTarget;

    private PlayerInput _playerInput;
    private PlayerWeaponOwnerComponent _weaponOwner;
    private CharacterController _characterController;
    private PlayerAnimationComponent _animationComponent;
    private AimingComponent _aimingComponent;

    public Transform EnemyTarget => _enemyTarget;
    public Vector2 Input { get; private set; } = Vector2.zero;

    private Vector3 _rootMotion;
    private Vector3 _velocity;
    private bool _isJumping;
    private bool _isSprinting = false;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animationComponent = GetComponent<PlayerAnimationComponent>();
        _aimingComponent = GetComponent<AimingComponent>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _weaponOwner = GetComponent<PlayerWeaponOwnerComponent>();

        _playerInput.Player.SelectWeapon_1.performed += _weaponOwner.SelectWeapon;
        _playerInput.Player.SelectWeapon_2.performed += _weaponOwner.SelectWeapon;


        _playerInput.Player.Shoot.started += _weaponOwner.StartFire;
        _playerInput.Player.Shoot.canceled += _weaponOwner.StopFire;

        _playerInput.Player.Holster.performed += _weaponOwner.Holster;
        _playerInput.Player.Reload.performed += _weaponOwner.Reload;

        _playerInput.Player.Jump.performed += Jump;


        _playerInput.Player.Sprint.performed += delegate { _isSprinting = true; };
        _playerInput.Player.Sprint.canceled += delegate { _isSprinting = false; };

        _playerInput.Player.Zoom.started += delegate { _aimingComponent.OnAimingStateChanged(true); };
        _playerInput.Player.Zoom.canceled += delegate { _aimingComponent.OnAimingStateChanged(false); };
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        if (Input != _playerInput.Player.Move.ReadValue<Vector2>())
        {
            Input = new Vector2(
                Mathf.Lerp(Input.x, _playerInput.Player.Move.ReadValue<Vector2>().x, 5f * Time.deltaTime),
                Mathf.Lerp(Input.y, _playerInput.Player.Move.ReadValue<Vector2>().y, 5f * Time.deltaTime));
        }

        UpdateIsSprinting();
    }

    private void OnAnimatorMove()
    {
        _rootMotion += _animationComponent.MeshAnimator.deltaPosition;
    }

    private void FixedUpdate()
    {
        if (_isJumping)
        {
            UpdateInAir();
        }
        else
        {
            UpdateOnGround();
        }
    }

    private bool IsSpringing()
    {
        return _isSprinting && (_weaponOwner.GetActiveWeapon() == null ||
                                (_weaponOwner.GetActiveWeapon() != null &&
                                 !_weaponOwner.GetActiveWeapon().IsFiring &&
                                 !_weaponOwner.ReloadComponent.IsReloading &&
                                 !_aimingComponent.IsAiming &&
                                 !_weaponOwner.IsWeaponChangeInProcess));
    }

    private void UpdateIsSprinting()
    {
        _animationComponent.SetSprintState(IsSpringing());
    }

    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = _rootMotion * _groundSpeed;
        Vector3 stepDownAmount = Vector3.down * _stepDown;

        _characterController.Move(stepForwardAmount + stepDownAmount);
        _rootMotion = Vector3.zero;

        if (!_characterController.isGrounded)
        {
            SetInAir(0);
        }
    }

    private void UpdateInAir()
    {
        _velocity.y -= _gravity * Time.fixedDeltaTime;

        Vector3 displacement = _velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        _characterController.Move(displacement);

        _isJumping = !_characterController.isGrounded;
        _rootMotion = Vector3.zero;

        _animationComponent.SetJumpingState(_isJumping);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!_isJumping)
        {
            SetInAir(Mathf.Sqrt(2 * _gravity * _jumpHeight));
        }
    }

    private void SetInAir(float jumpVelocity)
    {
        _isJumping = true;
        _velocity = _animationComponent.MeshAnimator.velocity * (_jumpDamp * _groundSpeed);
        _velocity.y = jumpVelocity;
        _animationComponent.SetJumpingState(true);
    }

    private Vector3 CalculateAirControl()
    {
        return ((transform.forward * Input.y) + (transform.right * Input.x)) * (_airControl / 100);
    }
}