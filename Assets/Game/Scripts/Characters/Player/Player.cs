using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private WallGrabDetector _wallGrabDetector;
    [SerializeField] private PlayerVisualizer _visualizer;
    [SerializeField] private CharacterTurner _turner;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputReader.JumpPerformed += OnJumpPerformed;
        _inputReader.DashPerformed += OnDashPerformed;
        _groundDetector.JustGrounded += _mover.DisableControlsOnGrounded;
    }

    private void OnDisable()
    {
        _inputReader.JumpPerformed -= OnJumpPerformed;
        _inputReader.DashPerformed -= OnDashPerformed;
        _groundDetector.JustGrounded -= _mover.DisableControlsOnGrounded;

    }

    private void FixedUpdate()
    {
        _mover.Move(_groundDetector.IsGrounded, _inputReader.Movement.x, _inputReader.IsWalking);
    }

    private void Update()
    {
        bool isGrabbingWall = _wallGrabDetector.CheckIsGrabbing(_groundDetector.IsGrounded, _turner.IsFacingRight);
        _visualizer.UpdateAnimatorParams(_groundDetector.IsGrounded, _mover.CurrentHorizontalVelocity, isGrabbingWall);
        _turner.Turn(_rigidbody);

        if (isGrabbingWall)
            _mover.DisableGravity();
        else 
            _mover.EnableGravity();
    }

    private void OnJumpPerformed()
    {
        if (_groundDetector.IsGrounded && _mover.CanMove)
        {
            _mover.Jump();
            _visualizer.OnJumped();
        }
    }

    private void OnDashPerformed()
    {
        if (_mover.CanDash)
        {
            _mover.Dash(_turner.IsFacingRight);
            _visualizer.OnDashed(_groundDetector.IsGrounded);
        }
    }
}
