using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : IState
{
    private readonly NavMeshAgent _navMeshAgent = null;
    private readonly Player _player = null;

    public ChasePlayer(NavMeshAgent navMeshAgent, Player player)
    {
        _navMeshAgent = navMeshAgent;
        _player = player;
    }
    
    public void Tick()
    {
        Debug.Log("Chasing...");
        _navMeshAgent.SetDestination(_player.transform.position);
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}