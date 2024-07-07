using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class AIComponentsHolder : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private NavMeshAgent _navMeshAgent;
    private BehaviourTreeRunner _behaviourRunner;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _behaviourRunner = GetComponent<BehaviourTreeRunner>();

    //    _healthComponent.Died += OnDied;
    }
    private void OnDisable()
    {
      //  _healthComponent.Died -= OnDied;
    }
    private void OnDied()
    {
        _navMeshAgent.enabled = false;
        _behaviourRunner.enabled = false;
    }
    

}
