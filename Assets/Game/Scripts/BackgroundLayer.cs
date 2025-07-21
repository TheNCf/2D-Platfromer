using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayer : MonoBehaviour
{
    [field: SerializeField] public float Displacement { get; private set; } = 1.0f;

    private SpriteRenderer _spriteRenderer;

    public Vector2 Size => _spriteRenderer.bounds.size / 3.0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
