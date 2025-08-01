using System;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public float StaggerTime { get; private set; } = 0.3f;

    private int _currentHealth;

    public event Action<int> HealthChanged;
    public event Action DamageTaken;
    public event Action BecameDead;

    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }

        private set
        {
            _currentHealth = value;
            HealthChanged?.Invoke(value);
        }
    }
    public bool IsAlive => CurrentHealth > 0;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual int TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        DamageTaken?.Invoke();

        if (IsAlive == false)
        {
            BecameDead?.Invoke();
            return damage + CurrentHealth;
        }

        return damage;
    }

    public virtual void Heal(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
    }
}
