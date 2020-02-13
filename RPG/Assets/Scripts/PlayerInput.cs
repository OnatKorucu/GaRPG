using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public static IPlayerInput Instance { get; set; }
    
    public float Vertical => Input.GetAxis("Vertical");
    
    public float Horizontal => Input.GetAxis("Horizontal");
    
    public float MouseX => Input.GetAxis("Mouse X");
    
    public bool PausePressed { get; }

    public event Action<int> HotkeyPressed;

    public event Action MoveModeTogglePressed;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Tick();
    }

    public void Tick()
    {
        CheckHotkeyPressed();
        CheckMoverSwitched();
    }

    private void CheckHotkeyPressed()
    {
        if (HotkeyPressed == null)
            return;

        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                HotkeyPressed(i);
            }
        }
    }
    
    private void CheckMoverSwitched()
    {
        if (MoveModeTogglePressed == null)
            return;

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            MoveModeTogglePressed();
        }
    }
}