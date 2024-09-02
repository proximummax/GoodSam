using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private AIStateID _initialState;

    [SerializeField] private AIAgentConfig _config;
    public AIAgentConfig Config { get => _config;  }
    public AIStateMachine StateMachine { get; private set; }

    [SerializeField] private NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }

    [SerializeField] private AIAnimationController _animationController;
    public AIAnimationController AnimationController { get => _animationController; }

    [SerializeField] private Ragdoll _ragdoll;
    public Ragdoll Ragdoll { get => _ragdoll; }

    [SerializeField] private AIHealth _healthComponent;
    public AIHealth HealthComponent { get => _healthComponent; }

    public ThirdPlayerController TargetEnemy { get; private set; }

    [SerializeField] private AIWeaponOwner _weaponOwner;
    public AIWeaponOwner WeaponOwner { get => _weaponOwner; }
    public AISensor AISensor { get; private set; }

    public LevelBounds LevelBounds { get; private set; }

    public AIStateID CurrentState;


    public void Initialize()
    {
        StateMachine = new AIStateMachine(this);
        StateMachine.RegisterState(new AIChasePlayerState());
        StateMachine.RegisterState(new AIDeathState());
        StateMachine.RegisterState(new AIIdleState());
        StateMachine.RegisterState(new AIFindWeaponState());
        StateMachine.RegisterState(new AIAttackState());
        StateMachine.RegisterState(new AIFindHealthState());
        StateMachine.RegisterState(new AIFindAmmoState());

        AISensor = GetComponent<AISensor>();
        LevelBounds = FindObjectOfType<LevelBounds>();
        TargetEnemy = FindObjectOfType<ThirdPlayerController>();



        StateMachine.ChangeState(_initialState);
    }


    private void Update()
    {
        StateMachine.Update();
        CurrentState = StateMachine.CurrentState;
        FaceTarget();

        AnimationController.SetSpeed(NavMeshAgent.velocity.magnitude);
    }
    public void FaceTarget()
    {
        if (NavMeshAgent.enabled)
        {
            var turnTowardNavSteeringTarget = NavMeshAgent.steeringTarget;
            Vector3 direction = (turnTowardNavSteeringTarget - _root.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            _root.transform.rotation = Quaternion.Slerp(_root.transform.rotation, lookRotation, Time.deltaTime * 2.0f);
        }
    }
}
