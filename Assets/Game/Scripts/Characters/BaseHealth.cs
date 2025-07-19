using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    public bool IsAlive => _currentHealth > 0;
    public event Action BecameDead;

    void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (IsAlive ==  false) 
            BecameDead?.Invoke();
    }

    public virtual void Heal(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
    }
}
