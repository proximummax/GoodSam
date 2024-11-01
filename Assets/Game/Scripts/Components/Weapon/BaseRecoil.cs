using Cinemachine;
using UnityEngine;

public class BaseRecoil : MonoBehaviour
{
    public float RecoilModifier { get; set; } = 1.0f;

    [SerializeField] private Vector2[] _recoilPattern;
    [SerializeField] private float _duration;

    private AimingComponent _aimingComponent;
    private Animator _rigController;
    private CinemachineImpulseSource _cameraShake;

    private float _elapsedTime;
    private int _currentPatternPart;
    private Vector2 _currentRecoil;

    public void Inititalize(AimingComponent aimingComponent, Animator rigController)
    {
        _aimingComponent = aimingComponent;
        _rigController = rigController;
    }
    public void GenerateRecoil(string weaponName)
    {
        _elapsedTime = _duration;
        _cameraShake.GenerateImpulse(Camera.main.transform.forward);

        if (_recoilPattern.Length > 0)
        {
            _currentRecoil = _recoilPattern[_currentPatternPart];
            _currentPatternPart = GetNextPattern(_currentPatternPart);
        }

        _rigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);
    }
    public void Reset()
    {
        _currentPatternPart = 0;
    }
    private void Awake()
    {
        _cameraShake = GetComponent<CinemachineImpulseSource>();
    }
    private int GetNextPattern(int current)
    {
        return (current + 1) % _recoilPattern.Length;
    }

    private void Update()
    {
        if (_elapsedTime > 0 && _recoilPattern.Length > 0)
        {
            _aimingComponent.YAxis.Value -= ((_currentRecoil.y*0.5f * Time.deltaTime) / _duration) * RecoilModifier;
            _aimingComponent.XAxis.Value -= ((_currentRecoil.x * 0.5f * Time.deltaTime) / _duration) * RecoilModifier;
            _elapsedTime -= Time.deltaTime;
        }
    }
}
