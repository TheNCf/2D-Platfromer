using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueSmoother
{
    private SmoothType _smoothType;
    private int _easingPower = 4;

    private IEnumerator _coroutine;

    public event Action<float> NumberChanged;
    public event Action Ended;

    public ValueSmoother(SmoothType smoothType)
    {
        _smoothType = smoothType;
    }

    public void SmoothNumberChange(ICoroutineStarter coroutineStarter, float start, float target, float duration)
    {
        if (duration == 0)
        {
            NumberChanged?.Invoke(target);
            return;
        }

        if (_coroutine != null)
            coroutineStarter.StopCoroutine(_coroutine);

        _coroutine = SmoothNumberChangeCoroutine(start, target, duration);
        coroutineStarter.StartCoroutine(_coroutine);
    }

    private IEnumerator SmoothNumberChangeCoroutine(float start, float target, float duration)
    {
        float elapsedTime = 0;
        float startValue = start;

        do
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / duration;
            normalizedPosition = Mathf.Clamp01(normalizedPosition);
            float easedNormalizedPosition = normalizedPosition;

            switch (_smoothType)
            {
                case SmoothType.Quad:
                    easedNormalizedPosition = 1 - Mathf.Pow(1 - normalizedPosition, _easingPower);
                    break;
            }

            float intermedaiteValue = Mathf.Lerp(startValue, target, easedNormalizedPosition);
            NumberChanged?.Invoke(intermedaiteValue);

            yield return null;
        }
        while (elapsedTime < duration);

        Ended?.Invoke();
    }
}
