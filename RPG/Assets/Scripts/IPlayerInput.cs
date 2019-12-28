using System;

public interface IPlayerInput
{
    event Action<int> HotkeyPressed;
    event Action MoveModeTogglePressed;
    
    void Tick();
    
    float Vertical { get; }
    float Horizontal { get; }
    float MouseX { get; }
}