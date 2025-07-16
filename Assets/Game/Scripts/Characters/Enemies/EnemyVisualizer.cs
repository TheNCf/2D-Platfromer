using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisualizer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimatorParameters();
    }

    private void SetAnimatorParameters()
    {
        _animator.SetFloat(EnemyAnimatorData.Params.Speed, Mathf.Abs(_rigidbody.velocity.x));
    }
}
