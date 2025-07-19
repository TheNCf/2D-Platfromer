using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private TargetFinder _targetFinder;
    [SerializeField] private EnemyPatroler _patroler;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyVisualizer _visualizer;
    [SerializeField] private CharacterTurner _turner;

    private EnemyStateMachine _stateMachine;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine(_mover);

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _health.DamageTaken += OnDamageTaken;
    }

    private void OnDisable()
    {
        _health.DamageTaken -= OnDamageTaken;
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

    private void OnDamageTaken()
    {
        _visualizer.OnHurt();
        _mover.DisableControls(_health.StaggerTime);
    }
}
