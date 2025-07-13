using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputRegisterer : MonoBehaviour
{
    private PlayerInputActions _inputActions;

    public Vector2 Movement { get; private set; }
    public bool IsWalking { get; private set; }

    public event Action JumpPerformed;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.Move.started += OnMove;
        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMove;
        _inputActions.Player.Walk.started += OnWalk;
        _inputActions.Player.Walk.canceled += OnWalk;
        _inputActions.Player.Jump.started += OnJump;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _inputActions.Player.Move.started -= OnMove;
        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= OnMove;
        _inputActions.Player.Walk.started -= OnWalk;
        _inputActions.Player.Walk.canceled -= OnWalk;
        _inputActions.Player.Jump.started -= OnJump;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    private void OnWalk(InputAction.CallbackContext context)
    {
        IsWalking = context.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        JumpPerformed?.Invoke();
    }
}
