using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : CharacterMover
{
    [SerializeField] private MovementStats _movementStats;
    [field: SerializeField] public float GroundControlsRecoverTime { get; private set; } = 0.5f;

    private Rigidbody2D _rigidbody;

    public bool IsDashing { get; private set; } = false;
    public bool CanDash { get; private set; } = true;
    public bool IsClimbing { get; private set; } = false;
    public float CurrentHorizontalVelocity { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(bool isGrounded, float horizontalInput, bool isWalking)
    {
        float acceleration = isGrounded ? _movementStats.GroundedAcceleration : _movementStats.AirAcceleration;
        float deceleration = isGrounded ? _movementStats.GroundedDeceleration : _movementStats.AirDeceleration;
        acceleration = IsDashing ? _movementStats.DashDrag : acceleration;
        deceleration = IsDashing ? _movementStats.DashDrag : deceleration;

        if (horizontalInput != 0 && CanMove)
        {
            float currentSpeed = isWalking ? _movementStats.WalkSpeed : _movementStats.RunSpeed;

            float targetHorizontalVelocity = currentSpeed * horizontalInput;

            CurrentHorizontalVelocity = Mathf.Lerp(_rigidbody.velocity.x, targetHorizontalVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            CurrentHorizontalVelocity = Mathf.Lerp(_rigidbody.velocity.x, 0, deceleration * Time.deltaTime);
        }

        _rigidbody.velocity = new Vector2(CurrentHorizontalVelocity, _rigidbody.velocity.y);
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _movementStats.JumpHeight);
    }

    public void Dash(bool isFacingRight)
    {
        DisableControls(_movementStats.DashDragOverrideTime);
        StartCoroutine(DisableDash(_movementStats.DashCooldown));
        StartCoroutine(SetDashing(_movementStats.DashDragOverrideTime));

        float dashDirection = isFacingRight ? 1 : -1;
        _rigidbody.velocity = new Vector2(dashDirection * _movementStats.DashForce, _rigidbody.velocity.y);
    }

    public void Climb(float time, bool isFacingRight)
    {
        float direction = isFacingRight ? 1 : -1;

        StartCoroutine(ClimbCoroutine(time, direction));
    }

    public void DisableGravity()
    {
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = Vector2.zero;
    }

    public void EnableGravity()
    {
        _rigidbody.gravityScale = _movementStats.GravityScale;
    }

    private IEnumerator SetDashing(float timeInSeconds)
    {
        DisableGravity();
        IsDashing = true;
        yield return new WaitForSeconds(timeInSeconds);
        IsDashing = false;
    }

    private IEnumerator DisableDash(float timeInSeconds)
    {
        CanDash = false;
        yield return new WaitForSeconds(timeInSeconds);
        CanDash = true;
    }

    private IEnumerator ClimbCoroutine(float time, float direction)
    {
        IsClimbing = true;
        yield return new WaitForSeconds(time);
        transform.Translate(1.0f * direction, 2.0f, 0.0f);
        IsClimbing = false;
    }
}