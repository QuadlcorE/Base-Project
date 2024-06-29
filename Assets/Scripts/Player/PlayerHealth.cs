using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// Reference to the powerup manager
    /// </summary>
    private PowerUpManager _powerUpManager;


    private enum Powerups
    {
        Regen,
        Juggernaut
    };

    /// <summary>
    /// Checks whether juggernaut or regen is the current powerup. 
    /// </summary>
    private PowerUpManager.Powerups _currentActivePowerup;

    /// <summary>
    /// Player's health regeneration rate (health per second)
    /// </summary>
    public float _regenRate;

    public readonly int MaxHealth = 100;

    public float CurrentHealth { get; private set; }


    private void Awake()
    {
        _powerUpManager = GetComponent<PowerUpManager>();
    }

    private void Start()
    {
        if (_powerUpManager != null)
        {
            _powerUpManager.OnPowerUpChanged += HandlePowerUpChanged;
        }

        _regenRate = 5;
        CurrentHealth = MaxHealth;
    }


    private void Update()
    {
        if (_currentActivePowerup == PowerUpManager.Powerups.Regen)
        {
            RegenerateHealth();
        }
    }

    private void HandlePowerUpChanged(PowerUpManager.Powerups newPowerUp)
    {
        switch (newPowerUp)
        {
            case PowerUpManager.Powerups.Regen:
                _currentActivePowerup = PowerUpManager.Powerups.Regen;
                break;

            case PowerUpManager.Powerups.Juggernaut:
                _currentActivePowerup = PowerUpManager.Powerups.Juggernaut;
                break;

            default:
                // Set to an invalid value because this script only needs to know when regen or juggernaut is active
                _currentActivePowerup = (PowerUpManager.Powerups)(-1);
                break;
        }
    }

    /// <summary>
    /// Regenerates player health to max health when regen is activated. 
    /// </summary>
    private void RegenerateHealth()
    {
        CurrentHealth += _regenRate * Time.deltaTime;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    internal void TakeDamage(float damage)
    {
        if (_currentActivePowerup == PowerUpManager.Powerups.Juggernaut)
        {
            damage /= 2;
        }
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            // Handle player death
            CurrentHealth = 0f;
            Debug.Log("Player died");
        }
    }

    private void OnDestroy()
    {
        if (_powerUpManager != null)
        {
            _powerUpManager.OnPowerUpChanged -= HandlePowerUpChanged;
        }
    }
}
