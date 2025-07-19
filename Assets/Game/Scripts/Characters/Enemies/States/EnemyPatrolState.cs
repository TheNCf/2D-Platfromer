using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private float _secondsToChange = 0.0f;
    private float _elapsedTime = 0.0f;

    public EnemyPatrolState(EnemyStateMachine stateMachine, EnemyMover mover) : base(stateMachine, mover)
    {

    }

    public override void Enter()
    {
        _secondsToChange = Random.Range(Mover.MinPatrolTime, Mover.MaxPatrolTime);
        _elapsedTime = 0.0f;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        Mover.Move();

        if (Mover.IsChasing)
            StateMachine.SetState(StateMachine.ChaseState);

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsToChange)
            StateMachine.SetState(StateMachine.WaitState);
    }
}
