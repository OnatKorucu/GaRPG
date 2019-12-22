using System;
using UnityEngine;

public interface IPlayerInput
{
    event Action<int> HotkeyPressed;
    event Action<KeyCode> MoverSwitched;
    
    void Tick();
    
    float Vertical { get; }
    float Horizontal { get; }
    float MouseX { get; }
}