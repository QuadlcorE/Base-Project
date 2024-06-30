using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;

    void Start()
    {
        InitializeSliderValues();
    }

    private void InitializeSliderValues()
    {
        _slider.maxValue = 100;
        _slider.minValue = 0;
    }

    public void UpdateSlider(int _sliderValue)
    {
        _slider.value = _sliderValue;
        _sliderText.text = _slider.value.ToString();
    }
}
