using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityViewBase : MonoBehaviour
{
    [SerializeField] private Ability _ability;

    protected ValueSmoother Smoother;

    protected virtual void Awake()
    {
        Smoother = new ValueSmoother(SmoothType.Linear);
    }

    private void OnEnable()
    {
        _ability.AbilityStarted += OnAbilityStart;
        _ability.AbilityEnded += OnAbilityEnd;
        Smoother.NumberChanged += OnAbilitySmoothNumberChange;
    }

    protected virtual void OnDisable()
    {
        _ability.AbilityStarted -= OnAbilityStart;
        _ability.AbilityEnded -= OnAbilityEnd;
        Smoother.NumberChanged -= OnAbilitySmoothNumberChange;
    }

    protected abstract void OnAbilityStart(float duration);

    protected abstract void OnAbilityEnd(float cooldown);

    protected abstract void OnAbilitySmoothNumberChange(float intermediateValue);
}
