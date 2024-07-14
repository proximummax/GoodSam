using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private AIStateID _initialState;
    [SerializeField] private AIAgentConfig _config;
    public AIAgentConfig Config { get { return _config; } }
    public AIStateMachine StateMachine { get; private set; }

    public NavMeshAgent NavMeshAgent { get; private set; }

    public AIAnimationController AnimationController { get; private set; }
    public Ragdoll Ragdoll { get; private set; }
    public AIHealthBar HealthBar { get; private set; }
    public HealthComponent HealthComponent { get; private set; }
    public ThirdPlayerController MainPlayer { get; private set; }
    public AIWeaponOwner WeaponOwner { get; private set; }
    private void Start()
    {
        StateMachine = new AIStateMachine(this);
        StateMachine.RegisterState(new AIChasePlayerState());
        StateMachine.RegisterState(new AIDeathState());
        StateMachine.RegisterState(new AIIdleState());
        StateMachine.RegisterState(new AIFindWeaponState());
        StateMachine.RegisterState(new AIAttackState());

        HealthBar = GetComponentInChildren<AIHealthBar>();
        Ragdoll = GetComponent<Ragdoll>();
        HealthComponent = GetComponent<HealthComponent>();
        AnimationController = GetComponent<AIAnimationController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        WeaponOwner = GetComponent<AIWeaponOwner>();

        MainPlayer = FindObjectOfType<ThirdPlayerController>();

      

        StateMachine.ChangeState(_initialState);

    }



    private void Update()
    {
        StateMachine.Update();
    }

}
