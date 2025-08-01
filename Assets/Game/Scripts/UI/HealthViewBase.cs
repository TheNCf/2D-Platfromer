using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthViewBase : MonoBehaviour
{
    [SerializeField] private BaseHealth _health;
    [field: SerializeField, Min(0.0f)] protected float SmoothDuration { get; private set; } 

    protected ValueSmoother Smoother;

    protected int MaxHealth => _health.MaxHealth;
    protected float CurrentValue => (float)_health.CurrentHealth / _health.MaxHealth;

    protected virtual void Awake()
    {
        Smoother = new ValueSmoother(SmoothType.Quad);
    }

    protected virtual void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        Smoother.NumberChanged += OnSmoothValueChanged;
    }

    protected virtual void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
        Smoother.NumberChanged -= OnSmoothValueChanged;
    }

    protected abstract void OnHealthChanged(int health);

    protected abstract void OnSmoothValueChanged(float intermediateValue);
}
