using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float _secondsToChange = 0.0f;
    private float _elapsedTime = 0.0f;

    float _attackTime = 0.75f;

    public EnemyAttackState(EnemyMover enemyMover) : base(enemyMover)
    {

    }

    public override void Enter()
    {
        _secondsToChange = _attackTime;
        _elapsedTime = 0.0f;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        _elapsedTime += Time.deltaTime;

        if ( _elapsedTime > _secondsToChange)
            _EnemyMover.SetState(_EnemyMover.StateMachine.ChaseState);
    }
}