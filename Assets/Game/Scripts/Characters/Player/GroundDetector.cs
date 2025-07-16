using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private MovementStats _movementStats;
    [SerializeField] private Collider2D _footCollider;
    [SerializeField] private LayerMask _groundLayerMask;

    public event Action JustGrounded;

    public bool IsGrounded { get; private set; }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        GetGroundBox(out Vector2 center, out Vector2 size);
        float angle = 0.0f;

        bool previousValue = IsGrounded;
        IsGrounded = Physics2D.OverlapBox(center, size, angle, _groundLayerMask);

        if (IsGrounded == true && previousValue == false)
            JustGrounded?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        if (_footCollider != null)
        {
            GetGroundBox(out Vector2 center, out Vector2 size);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, size);
        }
    }

    private void GetGroundBox(out Vector2 center, out Vector2 size)
    {
        center = _footCollider.bounds.center;
        center.y = _footCollider.bounds.min.y;
        float width = _footCollider.bounds.size.x;
        size = new Vector2(width, _movementStats.GroundBoxHeight);
    }
}
