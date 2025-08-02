using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerVampireAbility : Ability
{
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private float _radius = 6.0f;
    [SerializeField] private int _absorbtionAmount = 5;
    [SerializeField] private float _absorptionCooldown = 0.2f;

    private PlayerHealth _playerHealth;
    private EnemyHealth _closestEnemy;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public override float StartAbility()
    {
        float duration = base.StartAbility();

        if (duration > 0.0f)
        {
            StartCoroutine(AbsorbCoroutine());
        }

        return duration;
    }

    private void FindClosestEnemy()
    {
        float closestSqrDistance = float.MaxValue;
        _closestEnemy = null;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayerMask);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.TryGetComponent(out EnemyHealth enemyHealth))
            {
                float sqrDistance = transform.position.GetSqrDistance(enemyHealth.transform.position);

                if (closestSqrDistance > sqrDistance)
                    _closestEnemy = enemyHealth;
            }
        }
    }

    private IEnumerator AbsorbCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_absorptionCooldown);

        while (IsActive)
        {
            FindClosestEnemy();

            if (_closestEnemy != null && _closestEnemy.IsAlive)
                _playerHealth.TakeHeal(_closestEnemy.TakeDamage(_absorbtionAmount));

            yield return wait;
        }
    }
}
