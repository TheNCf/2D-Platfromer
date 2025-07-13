using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputRegisterer _inputRegisterer;
    [SerializeField] private MovementCharacteristics _movementValues;
    [Space(10)]
    [SerializeField] private Collider2D _footCollider;

    private Rigidbody2D _rigidbody;

    private bool _canMove = true;
    private float _groundBoxHeight = 0.05f;
    private float _groundControlRecoverTime = 0.5f;

    public bool IsGrounded { get; private set; } = true;
    public float CurrentHorizontalVelocity { get; private set; }

    public event Action Jumped;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputRegisterer.JumpPerformed += Jump;
    }

    private void OnDisable()
    {
        _inputRegisterer.JumpPerformed -= Jump;
    }

    private void Update()
    {
        GroundCheck();
        Move();
        Turn();
    }

    private void Move()
    {
        float acceleration = IsGrounded ? _movementValues.GroundedAcceleration : _movementValues.AirAcceleration;
        float deceleration = IsGrounded ? _movementValues.GroundedDeceleration : _movementValues.AirDeceleration;

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

    private void Turn()
    {
        if (_rigidbody.velocity.x > 0)
            transform.localEulerAngles = new Vector3(0, 0, 0);
        else if (_rigidbody.velocity.x < 0)
            transform.localEulerAngles = new Vector3(0, 180, 0);
    }

    private IEnumerator DisableControls(float timeInSeconds)
    {
        _canMove = false;
        yield return new WaitForSeconds(timeInSeconds);
        _canMove = true;
    }

    private void GetGroundBox(out Vector2 center, out Vector2 size)
    {
        center = _footCollider.bounds.center;
        center.y = _footCollider.bounds.min.y;
        float width = _footCollider.bounds.size.x;
        size = new Vector2(width, _groundBoxHeight);
    }

    private void GroundCheck()
    {
        GetGroundBox(out Vector2 center, out Vector2 size);
        float angle = 0.0f;

        bool previousValue = IsGrounded;
        IsGrounded = Physics2D.OverlapBoxAll(center, size, angle).Length > 1;

        if (IsGrounded == true && previousValue == false)
            StartCoroutine(DisableControls(_groundControlRecoverTime));
    }

    private void OnDrawGizmos()
    {
        if (_footCollider != null)
        {
            GetGroundBox(out Vector2 center, out Vector2 size);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, size);
        }
    }
}
