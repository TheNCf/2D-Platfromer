public abstract class EnemyBaseState
{
    protected EnemyMover EnemyMover;
    protected EnemyStateMachine StateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine, EnemyMover enemyMover)
    {
        StateMachine = stateMachine;
        EnemyMover = enemyMover;
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void Update();
}
