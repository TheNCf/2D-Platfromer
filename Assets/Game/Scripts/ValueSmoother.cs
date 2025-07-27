using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueSmoother
{
    private float _duration;
    private int _easingPower = 4;

    private IEnumerator _coroutine;

    public event Action<float> NumberChanged;

    public ValueSmoother(float duration)
    {
        _duration = duration;
    }

    public void SmoothNumberChange(MonoBehaviour coroutineStarter, float start, float target)
    {
        if (_duration == 0)
        {
            NumberChanged?.Invoke(target);
            return;
        }

        if (_coroutine != null)
            coroutineStarter.StopCoroutine(_coroutine);

        _coroutine = SmoothNumberChangeCoroutine(start, target);
        coroutineStarter.StartCoroutine(_coroutine);
    }

    private IEnumerator SmoothNumberChangeCoroutine(float start, float target)
    {
        float elapsedTime = 0;
        float startValue = start;

        do
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / _duration;
            normalizedPosition = Mathf.Clamp01(normalizedPosition);
            float easedNormalizedPosition = 1 - Mathf.Pow(1 - normalizedPosition, _easingPower);
            float intermedaiteValue = Mathf.Lerp(startValue, target, easedNormalizedPosition);
            NumberChanged?.Invoke(intermedaiteValue);

            yield return null;
        }
        while (elapsedTime < _duration);
    }
}
