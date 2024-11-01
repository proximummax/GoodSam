using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase _killCamera;

    public void EnableKillCamera()
    {
        _killCamera.Priority = 20;
    }
}
