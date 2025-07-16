using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyMover enemyMover) : base(enemyMover)
    {

    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        _EnemyMover.Move();

        if (_EnemyMover.IsChasing == false)
            _EnemyMover.SetState(_EnemyMover.StateMachine.WaitState);
    }
}
