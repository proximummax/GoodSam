using UnityEngine;

public class AIHealth : HealthComponent
{
  [SerializeField]  private AIAgent _agent;
    [SerializeField] private int lowHealth = 20;
    protected override void OnStart()
    {
      //  _agent = GetComponent<AIAgent>();
    }
    protected override void OnDeath()
    {
        _agent.StateMachine.ChangeState(AIStateID.Death);
        _agent.NavMeshAgent.enabled = false;
        _healthBar.gameObject.SetActive(false);
        OnDied?.Invoke();
    }
    protected override void OnDamage()
    {

    }
    public bool IsLowHealth()
    {
        return _currentHealth <= lowHealth;
    }
}
