using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitState : EnemyBaseState
{
    private float _secondsToChange = 0.0f;
    private float _elapsedTime = 0.0f;

    private float _minWaitTime = 1.0f;
    private float _maxWaitTime = 2.5f;

    public EnemyWaitState(EnemyMover enemyMover) : base(enemyMover)
    {

    }

    public override void Enter()
    {
        _secondsToChange = Random.Range(_minWaitTime, _maxWaitTime);
        _elapsedTime = 0.0f;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (_EnemyMover.IsChasing)
            _EnemyMover.SetState(_EnemyMover.StateMachine.ChaseState);

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsToChange)
            _EnemyMover.SetState(_EnemyMover.StateMachine.PatrolState);
    }
}
