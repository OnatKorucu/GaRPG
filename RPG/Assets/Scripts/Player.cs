using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    private IMover _mover;
    private Rotator _rotator;

    public IPlayerInput PlayerInput { get; set; } = new PlayerInput();

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mover = new Mover(this);
        _rotator = new Rotator(this);

        PlayerInput.MoverSwitched += HandleMoverSwitched;
    }

    private void HandleMoverSwitched(KeyCode keyCode)
    {
        if (keyCode == KeyCode.A)
        {
            _mover = new Mover(this);
        }
        if (keyCode == KeyCode.B)
        {
            _mover = new NavmeshMover(this);
        }
    }

    private void Update()
    {
        _mover.Tick();
        _rotator.Tick();

        PlayerInput.Tick();
    }
}