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
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    }
}
