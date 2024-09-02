using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "ScriptableObjects/Wave/WaveConfig", order = 1)]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private WaveUnit[] _waveUnits;
    public WaveUnit[] WaveUnits { get { return _waveUnits; } }

}
[System.Serializable]
public struct WaveUnit
{
    public Vector2 LevelsRange;
    public GameObject EnemyPrefab;
    public int Count;

}

