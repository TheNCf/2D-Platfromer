using UnityEngine;

public class PlayerVisualizer : MonoBehaviour
{
    [SerializeField] private InputRegisterer _inputRegisterer;
    [SerializeField] private PlayerMover _playerMover;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
        UpdateAnimatorParams();
        Turn();
    }

    private void OnJumped()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }

    private void OnDashed()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Dash);
    }

    private void UpdateAnimatorParams()
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(_playerMover.CurrentHorizontalVelocity));
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, _playerMover.IsGrounded);
    }

    private void Turn()
    {
        if (_playerMover.IsFacingRight)
            transform.localEulerAngles = new Vector3(0, 0, 0);
        else
            transform.localEulerAngles = new Vector3(0, 180, 0);
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