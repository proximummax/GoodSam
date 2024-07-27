using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaLevel : MonoBehaviour
{
    [SerializeField] private AIAgent _aiPrefab;
    [SerializeField] private int _countOfGeneratedEnemiesInFirstLevel;
    [SerializeField] private Transform[] _spawnPoints;

    private int _activeAI = 0;

    private void Awake()
    {
        _activeAI = _countOfGeneratedEnemiesInFirstLevel;
        for (int i = 0; i <  _spawnPoints.Length; i++)
        {
            AIAgent AiAgent =  Instantiate(_aiPrefab, _spawnPoints[i].position,Quaternion.identity, transform);
            AiAgent.Initialize();
            AiAgent.HealthComponent.OnDied += OnAIDied;
        }
       
    }
    private void OnAIDied()
    {
        if (_activeAI < 0)
            return;

        _activeAI--;
        if (_activeAI == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
