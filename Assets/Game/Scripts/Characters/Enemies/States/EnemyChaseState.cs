using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private EnemyAttacker _attacker;

    public EnemyChaseState(EnemyStateMachine stateMachine, EnemyMover mover, EnemyAttacker attacker) : base(stateMachine, mover)
    {
        _attacker = attacker;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (Mover.IsChasing == false)
        {
            StateMachine.SetState(StateMachine.WaitState);
            return;
        }

        if (Mover.transform.position.IsCloseToTarget(Mover.Target, _attacker.SqrDistanceToAttack))
        {
            StateMachine.SetState(StateMachine.AttackState);
            return;
        }

        Mover.Move();
    }
}
