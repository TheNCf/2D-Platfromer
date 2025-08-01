using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHealthView : HealthViewBase
{
    private TextMeshProUGUI _healthText;

    private float _currentValue;

    protected override void Awake()
    {
        base.Awake();
        _currentValue = CurrentValue;
        _healthText = GetComponent<TextMeshProUGUI>();
        UpdateText(_currentValue);
    }

    protected override void OnHealthChanged(int health)
    {
        Smoother.SmoothNumberChange(this, _currentValue, (float)health / MaxHealth, SmoothDuration);
    }

    protected override void OnSmoothValueChanged(float intermediateValue)
    {
        UpdateText(intermediateValue);
    }

    private void UpdateText(float value)
    {
        _currentValue = value;
        _healthText.text = Mathf.RoundToInt(value * MaxHealth) + "/" + MaxHealth;
    }
}
