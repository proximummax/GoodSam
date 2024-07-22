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
    public AIHealth HealthComponent { get; private set; }
    public ThirdPlayerController MainPlayer { get; private set; }
    public AIWeaponOwner WeaponOwner { get; private set; }
    public AISensor AISensor { get; private set; }
    public LevelBounds LevelBounds { get; private set; }

    public AIStateID CurrentState;
    private void Start()
    {
        StateMachine = new AIStateMachine(this);
        StateMachine.RegisterState(new AIChasePlayerState());
        StateMachine.RegisterState(new AIDeathState());
        StateMachine.RegisterState(new AIIdleState());
        StateMachine.RegisterState(new AIFindWeaponState());
        StateMachine.RegisterState(new AIAttackState());
        StateMachine.RegisterState(new AIFindHealthState());
        StateMachine.RegisterState(new AIFindAmmoState());

        HealthBar = GetComponentInChildren<AIHealthBar>();
        Ragdoll = GetComponent<Ragdoll>();
        HealthComponent = GetComponent<AIHealth>();
        AnimationController = GetComponent<AIAnimationController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        WeaponOwner = GetComponent<AIWeaponOwner>();
        AISensor = GetComponent<AISensor>();
        LevelBounds = FindObjectOfType<LevelBounds>();
        MainPlayer = FindObjectOfType<ThirdPlayerController>();

      

        StateMachine.ChangeState(_initialState);

    }



    private void Update()
    {
        StateMachine.Update();
        CurrentState = StateMachine.CurrentState;
    }

}
