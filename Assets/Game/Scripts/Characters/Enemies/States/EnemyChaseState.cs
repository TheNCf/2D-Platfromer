using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine stateMachine, EnemyMover enemyMover) : base(stateMachine, enemyMover)
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
        EnemyMover.Move();

        if (EnemyMover.IsChasing == false)
            StateMachine.SetState(StateMachine.WaitState);
    }
}
