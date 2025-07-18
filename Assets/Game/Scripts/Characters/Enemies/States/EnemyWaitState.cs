using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitState : EnemyBaseState
{
    private float _secondsToChange = 0.0f;
    private float _elapsedTime = 0.0f;

    private float _minWaitTime = 1.0f;
    private float _maxWaitTime = 2.5f;

    public EnemyWaitState(EnemyStateMachine stateMachine, EnemyMover enemyMover) : base(stateMachine, enemyMover)
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
        if (EnemyMover.IsChasing)
            StateMachine.SetState(StateMachine.ChaseState);

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsToChange)
            StateMachine.SetState(StateMachine.PatrolState);
    }
}
