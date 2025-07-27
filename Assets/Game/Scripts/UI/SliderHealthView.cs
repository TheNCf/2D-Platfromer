using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHealthView : HealthViewBase
{
    private Slider _slider;

    protected override void Awake()
    {
        base.Awake();
        _slider = GetComponent<Slider>();
    }

    protected override void OnHealthChanged(int health)
    {
        Smoother.SmoothNumberChange(this, _slider.value, (float)health / MaxHealth);
    }

    protected override void OnSmoothValueChanged(float intermediateValue)
    {
        _slider.value = intermediateValue;
    }
}
