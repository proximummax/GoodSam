using UnityEngine;

public class AimingComponent : MonoBehaviour
{

    [SerializeField] private float _turnSpeed;
    private Camera _camera;

    [SerializeField] private float _aimDuration = 0.3f;

    private void Start()
    {
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.fixedDeltaTime);
    }

}
