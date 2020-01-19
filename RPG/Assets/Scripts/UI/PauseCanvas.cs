using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    private void Awake()
    {
        _pausePanel.SetActive(false);
        GameStateMachine.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateMachine.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(IState state, IState previousState)
    {
        _pausePanel.SetActive(state is Pause);
    }
}
