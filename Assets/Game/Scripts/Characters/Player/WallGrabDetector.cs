using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _grabbingLayer;
    [SerializeField] private Collider2D _characterCollider;

    [SerializeField] private float _grabbingDistance = 0.05f;
    [SerializeField] private float _notGrabbingHeight = 0.05f;

    public bool IsGrabbingWall { get; private set; } = false;

    public void DetectIsGrabbing(bool isGrounded, bool isFacingRight)
    {
        if (isGrounded)
        {
            IsGrabbingWall = false;
            return;
        }

        float direction = isFacingRight ? 1 : -1;
        Bounds bounds = _characterCollider.bounds;
        Vector2 castPoint = new Vector2(direction * bounds.extents.x + bounds.center.x, bounds.max.y);

        if (Physics2D.Raycast(castPoint + Vector2.up * _notGrabbingHeight, Vector2.right * direction, _grabbingDistance, _grabbingLayer))
        {
            IsGrabbingWall = false;
            return;
        }

        IsGrabbingWall = Physics2D.Raycast(castPoint, Vector2.right * direction, _grabbingDistance, _grabbingLayer);
    }
}
