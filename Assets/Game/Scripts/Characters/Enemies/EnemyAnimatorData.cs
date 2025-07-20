using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyAnimatorData
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int Hurt = Animator.StringToHash(nameof(Hurt));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int Death = Animator.StringToHash(nameof(Death));
    }
}
