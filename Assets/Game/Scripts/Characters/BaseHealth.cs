using System;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IDamageable
{
    private int _currentValue;

    public event Action<int> ValueChanged;
    public event Action DamageTaken;
    public event Action BecameDead;

    [field: SerializeField, Min(1)] public int MaxValue { get; private set; }
    [field: SerializeField] public float StaggerTime { get; private set; } = 0.3f;

    public int CurrentValue
    {
        get
        {
            return _currentValue;
        }

        private set
        {
            _currentValue = value;
            ValueChanged?.Invoke(value);
        }
    }
    public bool IsAlive => CurrentValue > 0;

    private void Awake()
    {
        CurrentValue = MaxValue;
    }

    public virtual int TakeDamage(int damage)
    {
        CurrentValue -= damage;
        DamageTaken?.Invoke();

        if (IsAlive == false)
        {
            BecameDead?.Invoke();
            return damage + CurrentValue;
        }

        return damage;
    }

    public virtual void TakeHeal(int amount)
    {
        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0, MaxValue);
    }
}
