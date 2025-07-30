using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    [SerializeField] private float _duration = 6.0f;
    [SerializeField] private float _cooldown = 4.0f;

    public bool IsActive { get; private set; } = false;
    public bool IsOnCooldown { get; private set; } = false;

    public virtual void StartAbility(MonoBehaviour coroutineStarter)
    {
        if (IsOnCooldown == false)
        {
            coroutineStarter.StartCoroutine(AbilityCoroutine());
        }
    }

    private IEnumerator AbilityCoroutine()
    {
        IsActive = true;
        IsOnCooldown = true;
        yield return new WaitForSeconds(_duration);
        IsActive = false;
        yield return new WaitForSeconds(_cooldown);
        IsOnCooldown = false;
    }
}
