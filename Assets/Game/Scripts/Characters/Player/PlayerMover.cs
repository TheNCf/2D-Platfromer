using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private MovementStats _movementStats;

    private Rigidbody2D _rigidbody;

    private bool _isDashing = false;

    public bool CanMove { get; private set; } = true;
    public bool CanDash { get; private set; } = true;
    public float CurrentHorizontalVelocity { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(bool isGrounded, float horizontalInput, bool isWalking)
    {
        float acceleration = isGrounded ? _movementStats.GroundedAcceleration : _movementStats.AirAcceleration;
        float deceleration = isGrounded ? _movementStats.GroundedDeceleration : _movementStats.AirDeceleration;
        acceleration = _isDashing ? _movementStats.DashDrag : acceleration;
        deceleration = _isDashing ? _movementStats.DashDrag : deceleration;

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
        StartCoroutine(DisableDash(_movementStats.DashCooldown));
        StartCoroutine(SetDashing(_movementStats.DashDragOverrideTime));

        float dashDirection = isFacingRight ? 1 : -1;
        _rigidbody.velocity = new Vector2(dashDirection * _movementStats.DashForce, _rigidbody.velocity.y);
    }

    public void DisableControlsOnGrounded()
    {
        StartCoroutine(DisableControlsCoroutine(_movementStats.GroundControlRecoverTime));
    }

    private IEnumerator DisableControlsCoroutine(float timeInSeconds)
    {
        CanMove = false;
        yield return new WaitForSeconds(timeInSeconds);
        CanMove = true;
    }

    private IEnumerator SetDashing(float timeInSeconds)
    {
        _isDashing = true;
        yield return new WaitForSeconds(timeInSeconds);
        _isDashing = false;
    }

    private IEnumerator DisableDash(float timeInSeconds)
    {
        CanDash = false;
        yield return new WaitForSeconds(timeInSeconds);
        CanDash = true;
    }
}