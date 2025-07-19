using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisualizer : CharacterVisualizer
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private Animator _animator;

    private void Awake()
    {
        Initialize();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimatorParameters();
    }

    public void OnHurt()
    {
        _animator.SetTrigger(EnemyAnimatorData.Params.Hurt);
        DamageSpriteEffect();
    }

    public void OnAttack()
    {
        _animator.SetTrigger(EnemyAnimatorData.Params.Attack);
    }

    private void SetAnimatorParameters()
    {
        _animator.SetFloat(EnemyAnimatorData.Params.Speed, Mathf.Abs(_rigidbody.velocity.x));
    }
}
