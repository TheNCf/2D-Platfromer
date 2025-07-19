using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitState : EnemyBaseState
{
    private float _secondsToChange = 0.0f;
    private float _elapsedTime = 0.0f;

    public EnemyWaitState(EnemyStateMachine stateMachine, EnemyMover mover) : base(stateMachine, mover)
    {

    }

    public override void Enter()
    {
        _secondsToChange = Random.Range(Mover.MinWaitTime, Mover.MaxWaitTime);
        _elapsedTime = 0.0f;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (Mover.IsChasing)
            StateMachine.SetState(StateMachine.ChaseState);

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsToChange)
            StateMachine.SetState(StateMachine.PatrolState);
    }
}
