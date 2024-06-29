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
    private PlayerHealth _playerHealth;

    void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {
        InitializeSliderValues();
    }

    void Update()
    {
        UpdateSlider();
    }

    private void InitializeSliderValues()
    {
        _slider.maxValue = _playerHealth.MaxHealth;
        _slider.minValue = 0;
    }

    private void UpdateSlider()
    {
        _slider.value = (int) _playerHealth.CurrentHealth;
        _sliderText.text = _slider.value.ToString();
    }
}
