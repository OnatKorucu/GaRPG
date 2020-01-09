using System;
using UnityEngine;
using UnityEngine.AI;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;

    public Type CurrentStateType => _stateMachine.CurrentState.GetType();
    public event Action<IState> OnEntityStateChanged;
    

    private void Awake()
    {
        Player _player = FindObjectOfType<Player>();
        NavMeshAgent _navMeshAgent = GetComponent<NavMeshAgent>();
        Entity _entity = GetComponent<Entity>();
        
        _stateMachine = new StateMachine();
        _stateMachine.OnStateChanged += state => OnEntityStateChanged?.Invoke(state);
        
        Idle idle = new Idle();
        ChasePlayer chasePlayer = new ChasePlayer(_navMeshAgent, _player);
        Attack attack = new Attack();
        Dead dead = new Dead(_entity);
        
        _stateMachine.AddTransition(
            idle, 
            chasePlayer, 
            () => DistanceFlat(_navMeshAgent.transform.position, _player.transform.position) < 5f
                  );
        
        _stateMachine.AddTransition(
            chasePlayer, 
            attack, 
            () => DistanceFlat(_navMeshAgent.transform.position, _player.transform.position) < 2f
                  );
        
        _stateMachine.AddAnyTransition(
            dead, 
            () => _entity.Health <= 0
                  );
        
        _stateMachine.SetState(idle);
    }

    private float DistanceFlat(Vector3 source, Vector3 destination)
    {
        return Vector3.Distance(new Vector3(source.x, 0, source.z), new Vector3(destination.x, 0, destination.z));
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}