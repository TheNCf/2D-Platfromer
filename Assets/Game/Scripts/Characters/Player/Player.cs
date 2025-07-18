using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private WallGrabDetector _wallGrabDetector;
    [SerializeField] private PlayerAttacker _attacker;
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
        _inputReader.AttackPerformed += OnAttackPerformed;
        _groundDetector.JustGrounded += OnGrounded;
    }

    private void OnDisable()
    {
        _inputReader.JumpPerformed -= OnJumpPerformed;
        _inputReader.DashPerformed -= OnDashPerformed;
        _inputReader.AttackPerformed -= OnAttackPerformed;
        _groundDetector.JustGrounded -= OnGrounded;
    }

    private void FixedUpdate()
    {
        _mover.Move(_groundDetector.IsGrounded, _inputReader.Movement.x, _inputReader.IsWalking);
    }

    private void Update()
    {
        _wallGrabDetector.CheckIsGrabbing(_groundDetector.IsGrounded, _turner.FacingRight);
        _visualizer.UpdateAnimatorParams(_groundDetector.IsGrounded, _mover.CurrentHorizontalVelocity, _wallGrabDetector.IsGrabbingWall);
        _turner.Turn(_rigidbody);

        if (_wallGrabDetector.IsGrabbingWall)
            _mover.DisableGravity();
        else if (_mover.IsDashing == false)
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
            _mover.Dash(_inputReader.IsLastMovementRight);
            _visualizer.OnDashed(_groundDetector.IsGrounded);
        }
    }

    private void OnAttackPerformed()
    {
        if (_groundDetector.IsGrounded && _mover.IsDashing == false)
        {
            _visualizer.OnAttack();
            _mover.DisableControls(_attacker.AttackControlsRecoverTime);
        }

        if (_groundDetector.IsGrounded == false && _wallGrabDetector.IsGrabbingWall == false)
        {
            _visualizer.OnAttack();
        }
    }

    private void OnGrounded()
    {
        _mover.DisableControls(_mover.GroundControlsRecoverTime);
    }
}
