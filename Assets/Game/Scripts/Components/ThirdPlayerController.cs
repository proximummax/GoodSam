using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private WeaponOwnerComponent _weaponOwner;
    private Rigidbody _rigidbody;

    public Vector2 Input { get; private set; }
    public bool IsAiming { get; private set; }
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();
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
}
