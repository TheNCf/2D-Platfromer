using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private Collider2D _chaseTrigger;
    [SerializeField] private List<Transform> _patrolPoints;
    [Space(10)]
    [SerializeField] private float _movementSpeed = 3.0f;
    [SerializeField] private float _closeSqrDistance = 0.5f;

    private EnemyState _enemyState;

    private Rigidbody2D _rigidbody;

    private Vector3 _targetPosition;
    private Vector3 _chaseBoxCenter;
    private Vector2 _chaseBoxSize;
    private List<Vector3> _patrolPositions = new List<Vector3>();
    private int _currentPatrolPoint = 0;

    public event Action Waited;

    public bool IsChasing { get; private set; } = false;

    public void SetState(EnemyState newEnemyState)
    {
        _enemyState?.Exit();
        _enemyState = newEnemyState;
        _enemyState.Enter();
        StartCoroutine(Wait(_enemyState.SecondsToChange));
    }

    public void Move()
    {
        float direction = IsFacingRight() ? 1 : -1;
        _rigidbody.velocity = new Vector2(_movementSpeed * direction, _rigidbody.velocity.y);
    }

    private void Awake()
    {
        SetState(new EnemyStatePatrol(this));

        _rigidbody = GetComponent<Rigidbody2D>();

        _chaseBoxCenter = _chaseTrigger.bounds.center;
        _chaseBoxSize = _chaseTrigger.bounds.size;
        Destroy(_chaseTrigger.gameObject);

        foreach (Transform patrolPoint in _patrolPoints)
        {
            _patrolPositions.Add(patrolPoint.position);
            Destroy(patrolPoint.gameObject);
        }
    }

    private void Update()
    {
        FindTarget();
        _enemyState.Update();
    }

    private void FixedUpdate()
    {
        IsChasing = Physics2D.OverlapBox(_chaseBoxCenter, _chaseBoxSize, 0.0f, _targetLayerMask);
    }

    private void FindTarget()
    {
        if (IsChasing)
        {
            _targetPosition = _target.position;
        }
        else
        {
            if (IsCloseToTarget())
                _currentPatrolPoint = ++_currentPatrolPoint % _patrolPositions.Count;

            _targetPosition = _patrolPositions[_currentPatrolPoint];
        }
    }

    private bool IsCloseToTarget()
    {
        float sqrDistance = (_targetPosition - transform.position).sqrMagnitude;
        Debug.Log(sqrDistance);
        return sqrDistance < _closeSqrDistance;
    }

    private bool IsFacingRight()
    {
        return _targetPosition.x > transform.position.x;
    }

    private IEnumerator Wait(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        Waited?.Invoke();
    }
}
