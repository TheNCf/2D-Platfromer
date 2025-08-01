using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private WallGrabDetector _wallGrabDetector;
    [SerializeField] private PlayerAttacker _attacker;
    [SerializeField] private Ability _playerAbility;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private PlayerVisualizer _visualizer;
    [SerializeField] private CharacterTurner _turner;
    [SerializeField] private PlayerItemCollector _itemCollector;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void FixedUpdate()
    {
        if (_health.IsAlive == false || _mover.IsClimbing == true)
            return;

        _wallGrabDetector.DetectIsGrabbing(_groundDetector.IsGrounded, _turner.FacingRight);

        _mover.Move(_groundDetector.IsGrounded, _inputReader.Movement.x, _inputReader.IsWalking);

        if (_wallGrabDetector.IsGrabbingWall)
            _mover.DisableGravity();
        else if (_mover.IsDashing == false)
            _mover.EnableGravity();
    }

    private void Update()
    {
        if (_health.IsAlive == false)
            return;

        _visualizer.UpdateAnimatorParams(_groundDetector.IsGrounded, _mover.CurrentHorizontalVelocity, _wallGrabDetector.IsGrabbingWall, _playerAbility.IsActive);
        _turner.Turn(_rigidbody);

        if (_wallGrabDetector.IsGrabbingWall && _inputReader.Movement.y > 0.1f && _mover.IsClimbing == false)
        {
            _visualizer.OnWallClimbUp();
            _mover.Climb(_visualizer.ClimbTime, _turner.FacingRight);
        }
    }

    private void SubscribeToEvents()
    {
        _inputReader.JumpPerformed += OnJumpPerformed;
        _inputReader.DashPerformed += OnDashPerformed;
        _inputReader.AttackPerformed += OnAttackPerformed;
        _inputReader.UsePerformed += OnUsePerformed;
        _inputReader.AbilityPerformed += OnAbility;
        _groundDetector.JustGrounded += OnGrounded;
        _health.DamageTaken += OnDamageTaken;
        _health.BecameDead += OnDeath;
    }

    private void UnsubscribeFromEvents()
    {
        _inputReader.JumpPerformed -= OnJumpPerformed;
        _inputReader.DashPerformed -= OnDashPerformed;
        _inputReader.AttackPerformed -= OnAttackPerformed;
        _inputReader.UsePerformed -= OnUsePerformed;
        _inputReader.AbilityPerformed -= OnAbility;
        _groundDetector.JustGrounded -= OnGrounded;
        _health.DamageTaken -= OnDamageTaken;
        _health.BecameDead -= OnDeath;
    }

    private void OnJumpPerformed()
    {
        if (_groundDetector.IsGrounded && _mover.CanMove)
        {
            _mover.Jump();
            _visualizer.OnJumped();
        }

        if (_wallGrabDetector.IsGrabbingWall && _mover.IsClimbing == false)
        {
            _mover.JumpFromHanging(_turner.FacingRight);
            _visualizer.OnJumped();
        }
    }

    private void OnDashPerformed()
    {
        if (_mover.CanDash && _mover.IsClimbing == false && _wallGrabDetector.IsGrabbingWall == false)
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

    private void OnDamageTaken()
    {
        _visualizer.OnHurt();
        _mover.DisableControls(_health.StaggerTime);
    }

    private void OnUsePerformed()
    {
        if (_groundDetector.IsGrounded && _mover.IsDashing == false)
        {
            if (_itemCollector.CollectItem())
            {
                _visualizer.OnItemPickUp();
                _mover.DisableControls(_itemCollector.ItemPickUpTime);
            }
        }
    }

    private void OnAbility()
    {
        if (_mover.IsDashing == false && _groundDetector.IsGrounded)
        {
            float duration = _playerAbility.StartAbility();

            if (duration > 0)
            {
                _visualizer.StartVampireVFX(duration);
                _mover.DisableControls(duration);
            }
        }
    }

    private void OnGrounded()
    {
        _mover.DisableControls(_mover.GroundControlsRecoverTime);
    }

    private void OnDeath()
    {
        _visualizer.OnDeath();
        _mover.EnableGravity();
        UnsubscribeFromEvents();
    }
}
