using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private float _secondsToChange = 0.0f;
    private float _elapsedTime = 0.0f;

    private float _minPatrolTime = 1.0f;
    private float _maxPatrolTime = 2.5f;

    public EnemyPatrolState(EnemyMover enemyMover) : base(enemyMover)
    {

    }

    public override void Enter()
    {
        _secondsToChange = Random.Range(_minPatrolTime, _maxPatrolTime);
        _elapsedTime = 0.0f;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        _EnemyMover.Move();

        if (_EnemyMover.IsChasing)
            _EnemyMover.SetState(_EnemyMover.StateMachine.ChaseState);

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsToChange)
            _EnemyMover.SetState(_EnemyMover.StateMachine.WaitState);
    }
}
