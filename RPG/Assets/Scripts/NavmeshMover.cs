using UnityEngine;
using UnityEngine.AI;

public class NavmeshMover : IMover
{
    private readonly Player _player;
    private NavMeshAgent _navMeshAgent;

    public NavmeshMover(Player player)
    {
        _player = player;
        
        _navMeshAgent = player.GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = true;
    }
    
    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                _navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}