using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private TargetFinder _targetFinder;
    [SerializeField] private EnemyPatroler _patroler;
    [SerializeField] private CharacterTurner _turner;

    private EnemyStateMachine _stateMachine;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine(_mover);

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        Vector3 target;

        if (_targetFinder.IsChasing)
            target = _targetFinder.Target;
        else
            target = _patroler.GetCurrentTarget();

        _turner.Turn(_rigidbody);
        _mover.IsChasing = _targetFinder.IsChasing;
        _mover.SetTarget(target);

        _stateMachine.RunState();
    }
}
