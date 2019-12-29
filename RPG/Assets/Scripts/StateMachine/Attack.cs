using UnityEngine;

public class Attack : IState
{
    public void Tick()
    {
        Debug.Log("Attacking...");
    }
 
    public void OnEnter()
    {
    }
 
    public void OnExit()
    {
    }
}