public class EnemyStateMachine
{
    private EnemyBaseState _state;

    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyWaitState WaitState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    public EnemyStateMachine(EnemyMover mover, EnemyAttacker attacker)
    {
        PatrolState = new EnemyPatrolState(this, mover);
        WaitState = new EnemyWaitState(this, mover);
        ChaseState = new EnemyChaseState(this, mover, attacker);
        AttackState = new EnemyAttackState(this, mover, attacker);

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
