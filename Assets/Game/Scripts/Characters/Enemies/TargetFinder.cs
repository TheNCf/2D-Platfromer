using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private Collider2D _chaseTrigger;
    [SerializeField] private List<Transform> _patrolPoints;
    [Space(10)]
    [SerializeField] private float _closeSqrDistance = 0.1f;

    private Vector3 _chaseBoxCenter;
    private Vector2 _chaseBoxSize;
    private List<Vector3> _patrolPositions = new List<Vector3>();
    private int _currentPatrolPoint = 0;

    public Vector3 Target { get; private set; }
    public bool IsChasing { get; private set; } = false;

    void Awake()
    {
        _chaseBoxCenter = _chaseTrigger.bounds.center;
        _chaseBoxSize = _chaseTrigger.bounds.size;
        Destroy(_chaseTrigger);

        foreach (Transform patrolPoint in _patrolPoints)
        {
            _patrolPositions.Add(patrolPoint.position);
            Destroy(patrolPoint.gameObject);
        }
    }

    void FixedUpdate()
    {
        CheckForTargets();
    }

    private void CheckForTargets()
    {
        Collider2D collider = Physics2D.OverlapBox(_chaseBoxCenter, _chaseBoxSize, 0.0f, _targetLayerMask);

        IsChasing = collider != null;

        if (IsChasing)
        {
            Target = collider.transform.position;
        }
        else
        {
            if (IsCloseToTarget())
                _currentPatrolPoint = ++_currentPatrolPoint % _patrolPositions.Count;

            Target = _patrolPositions[_currentPatrolPoint];
        }
    }

    private bool IsCloseToTarget()
    {
        float sqrDistance = (Target - transform.position).sqrMagnitude;
        return sqrDistance < _closeSqrDistance;
    }
}
