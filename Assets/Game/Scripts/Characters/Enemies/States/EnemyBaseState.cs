public abstract class EnemyBaseState
{
    protected EnemyMover _EnemyMover;

    public EnemyBaseState(EnemyMover enemyMover)
    {
        _EnemyMover = enemyMover;
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void Update();
}
