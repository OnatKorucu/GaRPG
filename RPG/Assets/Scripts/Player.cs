﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    public PlayerInput PlayerInput { get; } = new PlayerInput();

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 movementInput = new Vector3(0, 0, PlayerInput.Vertical);
        Vector3 movement = transform.rotation * movementInput;
        _characterController.SimpleMove(movement);
    }
}