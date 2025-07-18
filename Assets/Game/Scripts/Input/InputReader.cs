using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInputActions _inputActions;

    public Vector2 Movement { get; private set; }
    public bool IsWalking { get; private set; }
    public bool IsLastMovementRight { get; private set; } = true;

    public event Action JumpPerformed;
    public event Action DashPerformed;
    public event Action AttackPerformed;

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
        _inputActions.Player.Dash.started += OnDash;
        _inputActions.Player.Attack.started += OnAttack;
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
        _inputActions.Player.Dash.started -= OnDash;
        _inputActions.Player.Attack.started -= OnAttack;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();

        if (Movement.x > 0)
            IsLastMovementRight = true;
        else if (Movement.x < 0)
            IsLastMovementRight = false;
    }

    private void OnWalk(InputAction.CallbackContext context)
    {
        IsWalking = context.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        JumpPerformed?.Invoke();
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        DashPerformed?.Invoke();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        AttackPerformed?.Invoke();
    }
}
