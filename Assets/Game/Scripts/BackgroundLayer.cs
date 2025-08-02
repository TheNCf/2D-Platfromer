using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundLayer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private float _widthMultiplier = 3.0f;

    [field: SerializeField] public float Displacement { get; private set; } = 1.0f;

    public Vector2 Size => _spriteRenderer.bounds.size / _widthMultiplier;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
