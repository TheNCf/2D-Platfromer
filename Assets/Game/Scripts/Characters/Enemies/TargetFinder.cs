using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private Collider2D _chaseTrigger;

    private Vector3 _chaseBoxCenter;
    private Vector2 _chaseBoxSize;

    public Vector3 Target { get; private set; }
    public bool IsChasing { get; private set; } = false;

    private void Awake()
    {
        _chaseBoxCenter = _chaseTrigger.bounds.center;
        _chaseBoxSize = _chaseTrigger.bounds.size;
        Destroy(_chaseTrigger);
    }

    private void FixedUpdate()
    {
        CheckForTargets();
    }

    public void CheckForTargets()
    {
        Collider2D collider = Physics2D.OverlapBox(_chaseBoxCenter, _chaseBoxSize, 0.0f, _targetLayerMask);

        IsChasing = collider;

        if (IsChasing)
            Target = collider.transform.position;
    }
}
