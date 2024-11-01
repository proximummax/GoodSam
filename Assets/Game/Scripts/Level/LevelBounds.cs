using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    [SerializeField] private Transform _max;
    public Transform Max => _max;

    [SerializeField] private Transform _min;
    public Transform Min => _min;
}
