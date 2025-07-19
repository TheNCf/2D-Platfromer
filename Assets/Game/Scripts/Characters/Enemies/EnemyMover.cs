using System.Collections;
using UnityEngine;

public class EnemyMover : CharacterMover
{
    [SerializeField] private float _movementSpeed = 1.5f;

    [field: SerializeField] public float MinPatrolTime { get; private set; } = 1.0f;
    [field: SerializeField] public float MaxPatrolTime { get; private set; } = 2.5f;
    [field: SerializeField] public float MinWaitTime { get; private set; } = 1.0f;
    [field: SerializeField] public float MaxWaitTime { get; private set; } = 2.5f;

    private Rigidbody2D _rigidbody;

    public Vector3 Target { get; private set; }
    public bool IsFacingRight { get; private set; }
    public bool IsChasing { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Vector3 target)
    {
        Target = target;
    }

    public void Move()
    {
        if (CanMove)
        {
            IsFacingRight = Target.x > transform.position.x;
            float direction = IsFacingRight ? 1 : -1;
            _rigidbody.velocity = new Vector2(_movementSpeed * direction, _rigidbody.velocity.y);
        }
    }
}
