using Cinemachine;
using UnityEngine;

public class BaseRecoil : MonoBehaviour
{
    [HideInInspector] public AimingComponent AimingComponent;
    [HideInInspector] public Animator _rigController;
    private CinemachineImpulseSource _cameraShake;


    [SerializeField] private Vector2[] _recoilPattern;

    [SerializeField] private float _duration;
    public float RecoilModifier = 1.0f;


    private float _elapsedTime;
    private int _currentPatternPart;

    private Vector2 _currentRecoil;

    private void Awake()
    {
        _cameraShake = GetComponent<CinemachineImpulseSource>();
    }
    public void Reset()
    {
        _currentPatternPart = 0;
    }
    private int NextPattern(int current)
    {
        return (current + 1) % _recoilPattern.Length;
    }
    public void GenerateRecoil(string weaponName)
    {
        _elapsedTime = _duration;
        _cameraShake.GenerateImpulse(Camera.main.transform.forward);

        if (_recoilPattern.Length > 0)
        {
            _currentRecoil = _recoilPattern[_currentPatternPart];
            _currentPatternPart = NextPattern(_currentPatternPart);
        }
        _rigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);
    }
    private void Update()
    {
        if (_elapsedTime > 0 && _recoilPattern.Length > 0)
        {
            AimingComponent.YAxis.Value -= ((_currentRecoil.y * Time.deltaTime) / _duration) * RecoilModifier;
            AimingComponent.XAxis.Value -= ((_currentRecoil.x * Time.deltaTime) / _duration) * RecoilModifier;
            _elapsedTime -= Time.deltaTime;
        }
    }
}
