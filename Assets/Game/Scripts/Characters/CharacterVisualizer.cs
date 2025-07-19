using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualizer : MonoBehaviour
{
    [SerializeField] private float _damageColorDuration = 0.15f;
    [SerializeField] private Color _damageColor;

    private SpriteRenderer _spriteRenderer;

    private IEnumerator _damageSpriteEffectCoroutine;

    private Color _defaultColor;

    public void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _defaultColor = _spriteRenderer.color;
    }

    public void DamageSpriteEffect()
    {
        if (_damageSpriteEffectCoroutine != null)
            StopCoroutine(_damageSpriteEffectCoroutine);

        _damageSpriteEffectCoroutine = DamageSpriteEffectCoroutine(_damageColorDuration);
        StartCoroutine(_damageSpriteEffectCoroutine);
    }

    private IEnumerator DamageSpriteEffectCoroutine(float duration)
    {
        _spriteRenderer.color = _damageColor;
        yield return new WaitForSeconds(duration);
        _spriteRenderer.color = _defaultColor;
    }
}
