public abstract class EnemyBaseState
{
    protected EnemyMover Mover;
    protected EnemyStateMachine StateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine, EnemyMover enemyMover)
    {
        StateMachine = stateMachine;
        Mover = enemyMover;
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void Update();
}
