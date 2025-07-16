using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private TargetFinder _targetChecker;
    [SerializeField] private CharacterTurner _turner;

    private void FixedUpdate()
    {
        _mover.SetTarget(_targetChecker.Target);
        _mover.IsChasing = _targetChecker.IsChasing;
        _turner.Turn(_mover.IsFacingRight);

        _mover.RunState();
    }
}
