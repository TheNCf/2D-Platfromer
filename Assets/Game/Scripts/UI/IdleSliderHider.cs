using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class IdleSliderHider : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private float _showDuration;
    [SerializeField] private float _fadeDuration;

    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _backgroundImage;

    private Slider _slider;
    private ValueSmoother _smoother;

    private float _currentOpacity = 0.0f;

    private IEnumerator _fadeOutCoroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _smoother = new ValueSmoother(SmoothType.Quad);
        StartCoroutine(FadeOut(0.0f));
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
        _smoother.NumberChanged += OnOpacityChanged;
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
        _smoother.NumberChanged -= OnOpacityChanged;
    }

    private void OnValueChanged(float value)
    {
        _smoother.SmoothNumberChange(this, _currentOpacity, 1.0f, _fadeDuration);

        if (_fadeOutCoroutine != null)
            StopCoroutine(_fadeOutCoroutine);

        _fadeOutCoroutine = FadeOut(_showDuration);
        StartCoroutine(_fadeOutCoroutine);
    }

    private void OnOpacityChanged(float value)
    {
        _currentOpacity = value;

        Color fillColor = _fillImage.color;
        Color backgroundColor = _backgroundImage.color;
        fillColor.a = _currentOpacity;
        backgroundColor.a = _currentOpacity;

        _fillImage.color = fillColor;
        _backgroundImage.color = backgroundColor;
    }

    private IEnumerator FadeOut(float delay)
    {
        yield return new WaitForSeconds(delay);
        _smoother.SmoothNumberChange(this, _currentOpacity, 0.0f, _fadeDuration);
    }
}
