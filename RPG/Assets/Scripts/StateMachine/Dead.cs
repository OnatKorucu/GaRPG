using UnityEngine;

public class Dead : IState
{
    public void Tick()
    {
        Debug.Log("Dead!");
    }
 
    public void OnEnter()
    {
    }
 
    public void OnExit()
    {
    }
}