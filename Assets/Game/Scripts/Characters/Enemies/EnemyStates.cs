using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyAI _enemyAI;

    public EnemyState(EnemyAI enemyAI)
    {
        _enemyAI = enemyAI;
    }

    protected float _secondsToChange = 0.0f;
    protected float _elapsedTime = 0.0f;

    public abstract void Enter();

    public abstract void Exit();

    public abstract void Update();
}

public class EnemyStatePatrol : EnemyState
{
    private float _minPatrolTime = 1.0f;
    private float _maxPatrolTime = 2.5f;

    public EnemyStatePatrol(EnemyAI enemyAI) : base(enemyAI)
    {

    }

    public override void Enter()
    {
        _secondsToChange = Random.Range(_minPatrolTime, _maxPatrolTime);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        _enemyAI.Move();

        if (_enemyAI.IsChasing)
            _enemyAI.SetState(new EnemyStateChase(_enemyAI));

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _maxPatrolTime)
            _enemyAI.SetState(new EnemyStateWait(_enemyAI));
    }
}

public class EnemyStateWait : EnemyState
{
    private float _minWaitTime = 1.0f;
    private float _maxWaitTime = 2.5f;

    public EnemyStateWait(EnemyAI enemyAI) : base(enemyAI)
    {

    }

    public override void Enter()
    {
        _secondsToChange = Random.Range(_minWaitTime, _maxWaitTime);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (_enemyAI.IsChasing)
            _enemyAI.SetState(new EnemyStateChase(_enemyAI));

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsToChange)
            _enemyAI.SetState(new EnemyStatePatrol(_enemyAI));
    }
}

public class EnemyStateChase : EnemyState
{
    public EnemyStateChase(EnemyAI enemyAI) : base(enemyAI)
    {

    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        _enemyAI.Move();

        if (_enemyAI.IsChasing == false)
            _enemyAI.SetState(new EnemyStateWait(_enemyAI));
    }
}

public class EnemyStateAttack : EnemyState
{
    float _attackTime = 0.75f;

    public EnemyStateAttack(EnemyAI enemyAI) : base(enemyAI)
    {

    }

    public override void Enter()
    {
        _secondsToChange = _attackTime;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        _elapsedTime += Time.deltaTime;

        if ( _elapsedTime > _secondsToChange)
            _enemyAI.SetState(new EnemyStatePatrol(_enemyAI));
    }
}