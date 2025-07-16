using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyWaitState WaitState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    public EnemyStateMachine(EnemyMover enemyMover)
    {
        PatrolState = new EnemyPatrolState(enemyMover);
        WaitState = new EnemyWaitState(enemyMover);
        ChaseState = new EnemyChaseState(enemyMover);
        AttackState = new EnemyAttackState(enemyMover);
    }
}
