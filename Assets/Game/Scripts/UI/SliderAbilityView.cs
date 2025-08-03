using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))] 
public class SliderAbilityView : AbilityViewBase, ICoroutineRunner
{
    private Slider _slider;

    protected override void Awake()
    {
        base.Awake();
        _slider = GetComponent<Slider>();
    }

    protected override void OnAbilityStart(float duration)
    {
        Smoother.SmoothNumberChange(this, _slider.value, _slider.minValue, duration);
    }

    protected override void OnAbilityEnd(float cooldown)
    {
        Smoother.SmoothNumberChange(this, _slider.value, _slider.maxValue, cooldown);
    }

    protected override void OnAbilitySmoothNumberChange(float intermediateValue)
    {
        _slider.value = intermediateValue;
    }
}
