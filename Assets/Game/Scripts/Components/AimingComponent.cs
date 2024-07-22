using UnityEngine;

public class AimingComponent : MonoBehaviour
{
    [SerializeField] private float _turnSpeed;
    private Camera _camera;

    [SerializeField] private float _aimDuration = 0.3f;

    [SerializeField] private Transform _lookAt;

    private PlayerAnimationComponent _animationComponent;
    private BaseWeaponOwnerComponent _weaponOwner;

    public Cinemachine.AxisState XAxis;
    public Cinemachine.AxisState YAxis;

    public bool IsAiming = false;
    private void Start()
    {
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        _animationComponent = GetComponent<PlayerAnimationComponent>();
        _weaponOwner = GetComponent<BaseWeaponOwnerComponent>();
    }
    private void UpdateAiming()
    {
        _animationComponent.SetAimingState(IsAiming);
        var weapon = _weaponOwner.GetActiveWeapon();
        if (weapon && weapon.Recoil)
        {
            weapon.Recoil.RecoilModifier = IsAiming ? 0.3f : 1.0f;
        }
    }
    public void OnAimingStateChanged(bool state)
    {
        IsAiming = state;
        UpdateAiming();

    }
    private void Update()
    {
        XAxis.Update(Time.deltaTime);
        YAxis.Update(Time.deltaTime);

        _lookAt.eulerAngles = new Vector3(YAxis.Value, XAxis.Value, 0.0f);

        float yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.fixedDeltaTime);
       

    }

}
