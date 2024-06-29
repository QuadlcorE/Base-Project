using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    /// <summary>
    /// List containing the powerups that the player has selected.
    /// </summary>
    private List<string> _selectedPowerUps;

    /// <summary>
    /// Index to track the currently active power-up.
    /// </summary>
    private int _currentPowerUpIndex;

    public enum Powerups
    {
        Turret,
        Canon,
        Regen,
        Juggernaut,
        DropBox
    };

    /// <summary>
    /// Checks for the current active powerup. 
    /// </summary>
    private Powerups _currentActivePowerUp;

    public event Action<Powerups> OnPowerUpChanged;

    private TextMeshProUGUI _currentActivePowerUpText;


    void Awake()
    {
        _selectedPowerUps = GetSelectedPowerUps();
        if (_selectedPowerUps == null || _selectedPowerUps.Count == 0)
        {
            Debug.LogWarning("No power-ups selected. Initializing with default power-ups.");
            _selectedPowerUps = new List<string> { "Turret", "Canon", "Regen" }; // Add default power-ups
        }
        _currentActivePowerUp = (Powerups)Enum.Parse(typeof(Powerups), _selectedPowerUps[0]);
        _currentPowerUpIndex = 0;
    }

    private void Start()
    {
        _currentActivePowerUpText = GameObject.FindWithTag("CurrentActivePowerUp").GetComponent<TextMeshProUGUI>();
        
        if (_currentActivePowerUpText != null)
        {
            _currentActivePowerUpText.text = _selectedPowerUps[0];
        }
        else
        {
            Debug.LogWarning("_currentPowerUpText is not assigned in the inspector.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePowerUps();
        }
    }

    /// <summary>
    /// Toggles to the next power-up in the list.
    /// </summary>
    private void TogglePowerUps()
    {
        if (_selectedPowerUps.Count > 0)
        {
            _currentPowerUpIndex = (_currentPowerUpIndex + 1) % _selectedPowerUps.Count;
            string currentPowerUp = _selectedPowerUps[_currentPowerUpIndex];
            SetCurrentPowerUp(currentPowerUp);
        }
    }

    private void SetCurrentPowerUp(string powerUp)
    {
        if (Enum.TryParse(powerUp, out PowerUpManager.Powerups newPowerUp))
        {
            _currentActivePowerUp = newPowerUp;
            if (_currentActivePowerUpText != null)
            {
                _currentActivePowerUpText.text = newPowerUp.ToString();
            }
            OnPowerUpChanged?.Invoke(_currentActivePowerUp);
        }
        else
        {
            Debug.LogError("Invalid power-up: " + powerUp);
        }
    }

    /// <summary>
    /// Gets the currently active power-up.
    /// </summary>
    /// <returns>The current power-up.</returns>
    public Powerups GetCurrentActivePowerUp()
    {
        return _currentActivePowerUp;
    }

    /// <summary>
    /// Gets all the selected powerups from PlayerPrefs
    /// </summary>
    /// <returns>list of strings containing the player's selected powerups</returns>
    public List<string> GetSelectedPowerUps()
    {
        string powerUpsString = PlayerPrefs.GetString("PowerUps", "");
        if (string.IsNullOrEmpty(powerUpsString))
        {
            Debug.LogWarning("No power-ups found in PlayerPrefs. Returning an empty list.");
            return new List<string>();
        }
        return powerUpsString.Split(',').ToList();
    }
}
