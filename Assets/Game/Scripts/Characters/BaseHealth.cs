using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    [field: SerializeField] public float StaggerTime { get; private set; } = 0.3f;

    private int _currentHealth;

    public event Action DamageTaken;
    public event Action BecameDead;

    public bool IsAlive => _currentHealth > 0;

    void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        DamageTaken?.Invoke();

        if (IsAlive ==  false) 
            BecameDead?.Invoke();
    }

    public virtual void Heal(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
    }
}
