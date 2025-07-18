using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroler : MonoBehaviour
{
    [SerializeField] private List<Transform> _patrolPoints;
    [Space(10)]
    [SerializeField] private float _sqrCloseDistance = 0.1f;

    private List<Vector3> _patrolPositions = new List<Vector3>();
    private int _currentPatrolPoint = 0;

    private void Awake()
    {
        foreach (Transform patrolPoint in _patrolPoints)
        {
            _patrolPositions.Add(patrolPoint.position);
            Destroy(patrolPoint.gameObject);
        }
    }

    public Vector3 GetCurrentTarget()
    {
        if (transform.position.IsCloseToTarget(_patrolPositions[_currentPatrolPoint], _sqrCloseDistance))
            _currentPatrolPoint = ++_currentPatrolPoint % _patrolPositions.Count;

        return _patrolPositions[_currentPatrolPoint];
    }
}
