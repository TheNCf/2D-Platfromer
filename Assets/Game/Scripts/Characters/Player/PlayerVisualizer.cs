using UnityEngine;

public class PlayerVisualizer : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
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

    public void UpdateAnimatorParams(bool isGrounded, float horizontalVelocity, bool isGrabbingWall)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(horizontalVelocity));
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
        _animator.SetBool(PlayerAnimatorData.Params.IsGrabbingWall, isGrabbingWall);
    }
}