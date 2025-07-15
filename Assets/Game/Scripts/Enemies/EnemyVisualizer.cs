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
        Turn();
    }

    private void Turn()
    {
        if (_rigidbody.velocity.x > 0)
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        if (_rigidbody.velocity.x < 0)
            transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }

    private void SetAnimatorParameters()
    {
        _animator.SetFloat(EnemyAnimatorData.Params.Speed, Mathf.Abs(_rigidbody.velocity.x));
    }
}

public static class EnemyAnimatorData
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    }
}
