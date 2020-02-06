using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{
    public Type CurrentStateType => _stateMachine.CurrentState.GetType();
    public static event Action<IState, IState> OnGameStateChanged;
    
    private static bool _initialized;
    private StateMachine _stateMachine;

    private void Awake()
    {
        if (_initialized)
        {
            Destroy(gameObject);
            return;
        }

        _initialized = true;
        DontDestroyOnLoad(gameObject);
        
        _stateMachine = new StateMachine();
        _stateMachine.OnStateChanged += (state, previousState) => OnGameStateChanged?.Invoke(state, previousState);
        
        Menu menu = new Menu();
        Load load = new Load();
        Pause pause = new Pause();
        Play play = new Play();
        
        _stateMachine.AddTransition(menu, load, () => PlayButton.LevelToLoad != null);
        _stateMachine.AddTransition(load, play, load.Finished);
        _stateMachine.AddTransition(play, pause, () => Input.GetKeyDown(KeyCode.Escape));
        _stateMachine.AddTransition(pause, play, () => Input.GetKeyDown(KeyCode.Escape));
        _stateMachine.AddTransition(pause, menu, () => RestartButton.Pressed);
        
        _stateMachine.SetState(menu);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}

public class Menu : IState
{
    public void Tick() { }

    public void OnEnter()
    {
        PlayButton.LevelToLoad = null;
        SceneManager.LoadSceneAsync("Menu");
    }

    public void OnExit() { }
}

public class Pause : IState
{
    public static bool Active { get; private set; }
    
    public void Tick() { }

    public void OnEnter()
    {
        Active = true;
        Time.timeScale = 0f;
    }

    public void OnExit()
    {
        Active = false;
        Time.timeScale = 1f;
    }
}

public class Play : IState
{
    public void Tick() { }

    public void OnEnter() { }

    public void OnExit() { }
}

public class Load : IState
{
    public bool Finished() => _operations.TrueForAll(t => t.isDone);
    private List<AsyncOperation> _operations = new List<AsyncOperation>();
    
    public void Tick() { }

    public void OnEnter()
    {
        _operations.Add(SceneManager.LoadSceneAsync(PlayButton.LevelToLoad));
        _operations.Add(SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive));
    }

    public void OnExit()
    {
        _operations.Clear();
    }
}
