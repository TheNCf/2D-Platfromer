using UnityEngine;

public class PlayerVisualizer : CharacterVisualizer
{
    [SerializeField] private AnimationClip _wallClimbUpClip;

    private Animator _animator;

    public float ClimbTime => _wallClimbUpClip.length;

    private void Awake()
    {
        Initialize();
        _animator = GetComponent<Animator>();
    }

    public void OnJumped()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }

    public void OnDashed(bool isGrounded)
    {
        if (isGrounded)
            _animator.SetTrigger(PlayerAnimatorData.Params.Slide);
        else
            _animator.SetTrigger(PlayerAnimatorData.Params.Dash);
    }

    public void OnAttack()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Attack);
    }

    public void OnHurt()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Hurt);
        DamageSpriteEffect();
    }

    public void OnDeath()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Death);
    }

    public void OnWallClimbUp()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.WallClimbUp);
    }

    public void OnItemPickUp()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.PickUpItem);
    }

    public void UpdateAnimatorParams(bool isGrounded, float horizontalVelocity, bool isGrabbingWall, bool isVampireAttack)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(horizontalVelocity));
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
        _animator.SetBool(PlayerAnimatorData.Params.IsGrabbingWall, isGrabbingWall);
        _animator.SetBool(PlayerAnimatorData.Params.IsVampireAttack, isVampireAttack);
    }
}