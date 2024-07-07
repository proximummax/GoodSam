using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private float _blinkIntensivity;
    [SerializeField] private float _blinkDuration;
    private float _blinkTimer;

    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private MeshRenderer[] _optionalMeshes;
    public void Apply()
    {
        _blinkTimer = _blinkDuration;
    }
    private void Update()
    {
        _blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(_blinkTimer / _blinkDuration);
        float intensivity = (lerp * _blinkIntensivity) + 1.0f;
        _skinnedMeshRenderer.material.color = Color.white * intensivity;
        if(_optionalMeshes.Length > 0)
        {
            foreach(var mesh in _optionalMeshes)
            {
                mesh.material.color = Color.white * intensivity;
            }
        }
    }
}
