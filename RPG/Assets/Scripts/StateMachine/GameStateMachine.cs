using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        
        Menu menu = new Menu();
        LoadLevel loadLevel = new LoadLevel();
        Pause pause = new Pause();
        Play play = new Play();
        
        _stateMachine.SetState(loadLevel);
        
        _stateMachine.AddTransition(loadLevel, play, loadLevel.Finished);
    }
}

public class Menu : IState
{
    public void Tick() { }

    public void OnEnter() { }

    public void OnExit() { }
}

public class Pause : IState
{
    public void Tick() { }

    public void OnEnter() { }

    public void OnExit() { }
}

public class Play : IState
{
    public void Tick() { }

    public void OnEnter() { }

    public void OnExit() { }
}

public class LoadLevel : IState
{
    public bool Finished() => _operations.TrueForAll(t => t.isDone);
    private List<AsyncOperation> _operations = new List<AsyncOperation>();
    
    public void Tick() { }

    public void OnEnter()
    {
        _operations.Add(SceneManager.LoadSceneAsync("Level01"));
        _operations.Add(SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive));
    }

    public void OnExit()
    {
        _operations.Clear();
    }
}
