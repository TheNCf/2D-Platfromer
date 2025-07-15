using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputRegisterer _inputRegisterer;
    [SerializeField] private MovementCharacteristics _movementValues;
    [Space(10)]
    [SerializeField] private Collider2D _footCollider;

    private Rigidbody2D _rigidbody;

    private bool _canMove = true;
    private bool _canDash = true;
    private bool _isFacingRight = true;
    private bool _isDashing = false;

    public bool IsGrounded { get; private set; } = true;
    public float CurrentHorizontalVelocity { get; private set; }
    public bool IsFacingRight => CheckFacingRight();

    public event Action Jumped;
    public event Action Dashed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputRegisterer.JumpPerformed += Jump;
        _inputRegisterer.DashPerformed += Dash;
    }

    private void OnDisable()
    {
        _inputRegisterer.JumpPerformed -= Jump;
        _inputRegisterer.DashPerformed -= Dash;
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Move();
    }

    private void Move()
    {
        float acceleration = IsGrounded ? _movementValues.GroundedAcceleration : _movementValues.AirAcceleration;
        float deceleration = IsGrounded ? _movementValues.GroundedDeceleration : _movementValues.AirDeceleration;
        acceleration = _isDashing ? _movementValues.DashDrag : acceleration;
        deceleration = _isDashing ? _movementValues.DashDrag : deceleration;

        if (_inputRegisterer.Movement.x != 0 && _canMove)
        {
            float currentSpeed = _inputRegisterer.IsWalking ? _movementValues.WalkSpeed : _movementValues.RunSpeed;

            float targetHorizontalVelocity = currentSpeed * _inputRegisterer.Movement.x;

            CurrentHorizontalVelocity = Mathf.Lerp(_rigidbody.velocity.x, targetHorizontalVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            CurrentHorizontalVelocity = Mathf.Lerp(_rigidbody.velocity.x, 0, deceleration * Time.deltaTime);
        }

        _rigidbody.velocity = new Vector2(CurrentHorizontalVelocity, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (IsGrounded && _canMove)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _movementValues.JumpHeight);
            Jumped.Invoke();
        }
    }

    private void Dash()
    {
        if (_canDash)
        {
            StartCoroutine(DisableDash(_movementValues.DashCooldown));
            StartCoroutine(SetDashing(_movementValues.DashDragOverrideTime));

            Dashed?.Invoke();

            float dashDirection = _isFacingRight ? 1 : -1;
            _rigidbody.velocity = new Vector2(dashDirection * _movementValues.DashForce, _rigidbody.velocity.y);
        }
    }

    private bool CheckFacingRight()
    {
        if (_rigidbody.velocity.x > _movementValues.AmountOfMovementForTurn)
            _isFacingRight = true;
        else if (_rigidbody.velocity.x < -_movementValues.AmountOfMovementForTurn)
            _isFacingRight = false;

        return _isFacingRight;
    }

    private IEnumerator SetDashing(float timeInSeconds)
    {
        _isDashing = true;
        yield return new WaitForSeconds(timeInSeconds);
        _isDashing = false;
    }

    private IEnumerator DisableControls(float timeInSeconds)
    {
        _canMove = false;
        yield return new WaitForSeconds(timeInSeconds);
        _canMove = true;
    }

    private IEnumerator DisableDash(float timeInSeconds)
    {
        _canDash = false;
        yield return new WaitForSeconds(timeInSeconds);
        _canDash = true;
    }

    private void GetGroundBox(out Vector2 center, out Vector2 size)
    {
        center = _footCollider.bounds.center;
        center.y = _footCollider.bounds.min.y;
        float width = _footCollider.bounds.size.x;
        size = new Vector2(width, _movementValues.GroundBoxHeight);
    }

    private void GroundCheck()
    {
        GetGroundBox(out Vector2 center, out Vector2 size);
        float angle = 0.0f;

        bool previousValue = IsGrounded;
        IsGrounded = Physics2D.OverlapBoxAll(center, size, angle).Length > 1;

        if (IsGrounded == true && previousValue == false)
            StartCoroutine(DisableControls(_movementValues.GroundControlRecoverTime));
    }

    private void OnDrawGizmosSelected()
    {
        if (_footCollider != null)
        {
            GetGroundBox(out Vector2 center, out Vector2 size);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, size);
        }
    }
}