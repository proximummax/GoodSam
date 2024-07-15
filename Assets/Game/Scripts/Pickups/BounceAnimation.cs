using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    [SerializeField] private float _bounceSpeed = 8;
    [SerializeField] private float _bounceAmplitude = 0.05f;
    [SerializeField] private float _rotationSpeed = 90;

    private float _startingHeight;
    private float _timeOffset;

    private void Start()
    {
        _startingHeight = transform.localPosition.y;
        _timeOffset = Random.value * Mathf.PI * 2;
    }
    private void Update()
    {

        UpdatePosition();
        UpdateRotation();

    }
    private void UpdatePosition()
    {
        float finalPosition = _startingHeight + Mathf.Sin(Time.time * _bounceSpeed + _timeOffset) * _bounceAmplitude;
        var pos = transform.localPosition;
        pos.y = finalPosition;
        transform.localPosition = pos;
    }
    private void UpdateRotation()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.y += _rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(rotation);
    }
}
