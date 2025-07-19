using System.Collections;
using UnityEngine;

public class EnemyMover : CharacterMover
{
    [SerializeField] private float _movementSpeed = 1.5f;

    private Rigidbody2D _rigidbody;

    private Vector3 _target;

    public bool IsFacingRight { get; private set; }
    public bool IsChasing { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    public void Move()
    {
        if (CanMove)
        {
            IsFacingRight = _target.x > transform.position.x;
            float direction = IsFacingRight ? 1 : -1;
            _rigidbody.velocity = new Vector2(_movementSpeed * direction, _rigidbody.velocity.y);
        }
    }
}
