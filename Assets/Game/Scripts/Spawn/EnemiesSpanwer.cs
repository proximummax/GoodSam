using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemiesSpanwer : MonoBehaviour
{
    [SerializeField] private UnityEvent _onAllEnemiesDied;

    [SerializeField] private WaveConfig[] _wavesConfigs;
    [SerializeField] public float _spawnRadius = 10.0f;

    private int _activeAI = 0;
    private void Start()
    {
        int activeWaveIndex = UserSettingsStorage.Instance.Round < _wavesConfigs.Length ? UserSettingsStorage.Instance.Round : _wavesConfigs.Length - 1;

        SpawnWave(_wavesConfigs[activeWaveIndex]);
    }

    private void SpawnWave(WaveConfig waveData)
    {
        Vector3 point;
        bool spawned;
        foreach (var waveUnitData in waveData.WaveUnits)
        {
            for (int i = 0; i < waveUnitData.Count; i++)
            {
                spawned = false;

                while (!spawned)
                {
                    if (RandomPoint(transform.position, _spawnRadius, out point))
                    {
                        var enemyGameObject = Instantiate(waveUnitData.EnemyPrefab, point, Quaternion.identity, transform);
                        AIAgent AiAgent = enemyGameObject.GetComponentInChildren<AIAgent>();
                        AiAgent.Initialize();

                        AiAgent.HealthComponent.SetHealth(100 + UserSettingsStorage.Instance.Round * 10);
                        AiAgent.HealthComponent.OnDied.AddListener(OnAIDied);
                        spawned = true;
                        _activeAI++;
                    }

                }



            }
        }
    }

    private void OnAIDied()
    {
        if (_activeAI < 0)
            return;

        _activeAI--;
        if (_activeAI == 0)
        {
            _onAllEnemiesDied?.Invoke();
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
