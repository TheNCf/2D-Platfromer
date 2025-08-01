using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private float _duration = 6.0f;
    [SerializeField] private float _cooldown = 4.0f;

    public Action<float> AbilityStarted;
    public Action<float> AbilityEnded;

    public bool IsActive { get; private set; } = false;
    public bool IsOnCooldown { get; private set; } = false;

    public virtual float StartAbility()
    {
        if (IsOnCooldown == false)
        {
            StartCoroutine(AbilityCoroutine());
            return _duration;
        }

        return 0;
    }

    private IEnumerator AbilityCoroutine()
    {
        AbilityStarted?.Invoke(_duration);
        IsActive = true;
        IsOnCooldown = true;
        yield return new WaitForSeconds(_duration);

        AbilityEnded?.Invoke(_cooldown);
        IsActive = false;
        yield return new WaitForSeconds(_cooldown);
        IsOnCooldown = false;
    }
}
