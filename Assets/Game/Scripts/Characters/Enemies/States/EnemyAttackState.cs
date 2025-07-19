using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private EnemyAttacker _attacker;

    private float _elapsedTime = 0.0f;
    private float _attackTime;

    public EnemyAttackState(EnemyStateMachine stateMachine, EnemyMover mover, EnemyAttacker attacker) : base(stateMachine, mover)
    {
        _attacker = attacker;
    }

    public override void Enter()
    {
        _elapsedTime = 0.0f;
        _attackTime = _attacker.Attack();
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        _elapsedTime += Time.deltaTime;

        if ( _elapsedTime > _attackTime)
            StateMachine.SetState(StateMachine.ChaseState);
    }
}