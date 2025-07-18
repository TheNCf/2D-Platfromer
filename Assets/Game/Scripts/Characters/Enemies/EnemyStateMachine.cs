public class EnemyStateMachine
{
    private EnemyBaseState _state;

    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyWaitState WaitState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    public EnemyStateMachine(EnemyMover enemyMover)
    {
        PatrolState = new EnemyPatrolState(this, enemyMover);
        WaitState = new EnemyWaitState(this, enemyMover);
        ChaseState = new EnemyChaseState(this, enemyMover);
        AttackState = new EnemyAttackState(this, enemyMover);

        SetState(WaitState);
    }

    public void SetState(EnemyBaseState newEnemyState)
    {
        _state?.Exit();
        _state = newEnemyState;
        _state.Enter();
    }

    public void RunState()
    {
        _state.Update();
    }
}
