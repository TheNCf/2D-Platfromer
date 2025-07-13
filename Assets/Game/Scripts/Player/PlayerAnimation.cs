using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private InputRegisterer _inputRegisterer;

    private Animator _animator;
    private PlayerMover _playerMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _playerMover.Jumped += OnJumped;
        _playerMover.Dashed += OnDashed;
    }

    private void OnDisable()
    {
        _playerMover.Jumped -= OnJumped;
        _playerMover.Dashed -= OnDashed;
    }

    private void Update()
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(_playerMover.CurrentHorizontalVelocity));
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, _playerMover.IsGrounded);
    }

    private void OnJumped()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }

    private void OnDashed()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Dash);
    }
}

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int Dash = Animator.StringToHash(nameof(Dash));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    }
}