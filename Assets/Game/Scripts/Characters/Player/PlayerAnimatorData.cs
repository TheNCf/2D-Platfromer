using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int Dash = Animator.StringToHash(nameof(Dash));
        public static readonly int Slide = Animator.StringToHash(nameof(Slide));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int IsVampireAttack = Animator.StringToHash(nameof(IsVampireAttack));
        public static readonly int Hurt = Animator.StringToHash(nameof(Hurt));
        public static readonly int Death = Animator.StringToHash(nameof(Death));
        public static readonly int WallClimbUp = Animator.StringToHash(nameof(WallClimbUp));
        public static readonly int PickUpItem = Animator.StringToHash(nameof(PickUpItem));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
        public static readonly int IsGrabbingWall = Animator.StringToHash(nameof(IsGrabbingWall));
    }
}
