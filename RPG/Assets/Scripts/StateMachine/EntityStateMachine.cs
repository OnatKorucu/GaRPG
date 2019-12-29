using UnityEngine;
using UnityEngine.AI;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        _stateMachine = new StateMachine();
        
        Idle idle = new Idle();
        ChasePlayer chasePlayer = new ChasePlayer(_navMeshAgent);
        Attack attack = new Attack();
        
        _stateMachine.Add(idle);
        _stateMachine.Add(chasePlayer);
        _stateMachine.Add(attack);
     
        _stateMachine.AddTransition(
            idle, 
            chasePlayer, 
            () => Vector3.Distance(_navMeshAgent.transform.position, _player.transform.position) < 5f);
        
        _stateMachine.AddTransition(
            chasePlayer, 
            attack, 
            () => Vector3.Distance(_navMeshAgent.transform.position, _player.transform.position) < 2f);
        
        _stateMachine.SetState(idle);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}