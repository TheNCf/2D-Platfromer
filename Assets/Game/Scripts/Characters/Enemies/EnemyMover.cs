using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1.5f;

    private Rigidbody2D _rigidbody;

    private EnemyBaseState _state;

    private Vector3 _target;

    public EnemyStateMachine StateMachine { get; private set; }
    public bool IsFacingRight { get; private set; }
    public bool IsChasing { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        StateMachine = new EnemyStateMachine(this);
        SetState(StateMachine.WaitState);
    }

    public void SetState(EnemyBaseState newEnemyState)
    {
        _state?.Exit();
        _state = newEnemyState;
        _state.Enter();
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    public void Move()
    {
        IsFacingRight = _target.x > transform.position.x;
        float direction = IsFacingRight ? 1 : -1;
        _rigidbody.velocity = new Vector2(_movementSpeed * direction, _rigidbody.velocity.y);
    }

    public void RunState()
    {
        _state.Update();
    }
}
