using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimingComponent : MonoBehaviour
{
    private ThirdPlayerController _playerController;

    [SerializeField] private float _turnSpeed;
    private Camera _camera;

    [SerializeField] private float _aimDuration = 0.3f;
    [SerializeField] private Rig _aimRig;
    private void Start()
    {
        _playerController = GetComponent<ThirdPlayerController>();
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.fixedDeltaTime);


        // _aimRig.weight = _playerController.IsAiming ? _aimRig.weight + Time.deltaTime / _aimDuration : _aimRig.weight - Time.deltaTime / _aimDuration;

        _aimRig.weight = 1.0f;
    }

}
