using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisualizer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _damageColorDuration = 0.15f;
    [SerializeField] private Color _damageColor;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private IEnumerator _damageSpriteEffectCoroutine;

    private Color _defaultColor;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _defaultColor = _spriteRenderer.color;
    }

    private void Update()
    {
        SetAnimatorParameters();
    }

    public void OnHurt()
    {
        _animator.SetTrigger(EnemyAnimatorData.Params.Hurt);
        DamageSpriteEffect(_damageColorDuration);
    }

    private void SetAnimatorParameters()
    {
        _animator.SetFloat(EnemyAnimatorData.Params.Speed, Mathf.Abs(_rigidbody.velocity.x));
    }

    private void DamageSpriteEffect(float duration)
    {
        if (_damageSpriteEffectCoroutine != null)
            StopCoroutine(_damageSpriteEffectCoroutine);

        _damageSpriteEffectCoroutine = DamageSpriteEffectCoroutine(duration);
        StartCoroutine(_damageSpriteEffectCoroutine);
    }

    private IEnumerator DamageSpriteEffectCoroutine(float duration)
    {
        _spriteRenderer.color = _damageColor;
        yield return new WaitForSeconds(duration);
        _spriteRenderer.color = _defaultColor;
    }
}
