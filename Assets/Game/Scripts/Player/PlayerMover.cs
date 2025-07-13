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

    private bool _isGrounded = true;
    private float _groundBoxHeight = 0.05f;

    private void Awake()
    {
        _inputRegisterer.JumpPerformed += Jump;

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GroundCheck();
        Move();
        Turn();
    }

    private void Move()
    {
        float currentHorizontalVelocity;

        float acceleration = _isGrounded ? _movementValues.GroundedAcceleration : _movementValues.AirAcceleration;
        float deceleration = _isGrounded ? _movementValues.GroundedDeceleration : _movementValues.AirDeceleration;

        if (_inputRegisterer.Movement.x != 0)
        {
            float currentSpeed = _inputRegisterer.IsWalking ? _movementValues.WalkSpeed : _movementValues.RunSpeed;
            float targetHorizontalVelocity = currentSpeed * _inputRegisterer.Movement.x;

            currentHorizontalVelocity = Mathf.Lerp(_rigidbody.velocity.x, targetHorizontalVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentHorizontalVelocity = Mathf.Lerp(_rigidbody.velocity.x, 0, deceleration * Time.deltaTime);
        }

        _rigidbody.velocity = new Vector2(currentHorizontalVelocity, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _movementValues.JumpHeight);
        }
    }

    private void Turn()
    {
        if (_rigidbody.velocity.x > 0)
            transform.localEulerAngles = new Vector3(0, 0, 0);
        else if (_rigidbody.velocity.x < 0)
            transform.localEulerAngles = new Vector3(0, 180, 0);
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

        _isGrounded = Physics2D.OverlapBoxAll(center, size, angle).Length > 1;
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
