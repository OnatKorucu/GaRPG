using System;
using UnityEngine;

namespace a_player
{
    public class TestPlayerInput : IPlayerInput
    {
        public event Action<int> HotkeyPressed;
        public event Action MoveModeTogglePressed;
        public void Tick()
        {
            // do nothing
        }

        public float Vertical { get; set; }
        
        public float Horizontal { get; set; }

        public float MouseX { get; set; }
    }
}