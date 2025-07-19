using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int _attackDamage = 30;
    [SerializeField] private float _attackTime  = 0.75f;
    [field: SerializeField] public float SqrDistanceToAttack { get; private set; } = 2.5f;

    public event Action Attacking;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(_attackDamage);
        }
    }

    public float Attack()
    {
        Attacking?.Invoke();
        return _attackTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float lineHalfHeight = 1f;
        float distance = Mathf.Sqrt(SqrDistanceToAttack);
        Vector2 startPoint = transform.position + Vector3.up * lineHalfHeight;
        Vector2 endPoint = transform.position - Vector3.up * lineHalfHeight;

        Gizmos.DrawLine(startPoint + Vector2.right * distance, endPoint + Vector2.right * distance);
        Gizmos.DrawLine(startPoint + Vector2.left * distance, endPoint + Vector2.left * distance);
    }
}
