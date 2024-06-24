using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    /// <summary>
    /// Array containing the powerups that the player has selected.
    /// </summary>
    private List<string> _selectedPowerUps;

    /// <summary>
    /// Index to track the currently active power-up.
    /// </summary>
    private int _currentPowerUpIndex = 0;

    public bool HasTurret { get; private set; }
    public bool HasJuggernaut { get; private set; }
    public bool HasCanon { get; private set; }
    public bool HasRegen { get; private set; }
    public bool HasDropBox { get; private set; }

    void Awake()
    {
        _selectedPowerUps = GetPowerUps().ToList();

        if (_selectedPowerUps != null)
        {
            HasTurret = _selectedPowerUps.Contains("Turret");
            HasJuggernaut = _selectedPowerUps.Contains("Juggernaut");
            HasCanon = _selectedPowerUps.Contains("Canon");
            HasRegen = _selectedPowerUps.Contains("Regen");
            HasDropBox = _selectedPowerUps.Contains("Drop Box");
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
        if (_selectedPowerUps.Count == 0)
            return;

        _currentPowerUpIndex = (_currentPowerUpIndex + 1) % _selectedPowerUps.Count;
        string currentPowerUp = _selectedPowerUps[_currentPowerUpIndex];
        Debug.Log("Current Power-Up: " + currentPowerUp);

        // Here you can add code to update the player's current power-up based on `currentPowerUp`
    }

    /// <summary>
    /// Gets the currently active power-up.
    /// </summary>
    /// <returns>The current power-up as a string.</returns>
    public string GetCurrentPowerUp()
    {
        if (_selectedPowerUps.Count == 0)
            return string.Empty;

        return _selectedPowerUps[_currentPowerUpIndex];
    }

    public string[] GetPowerUps()
    {
        return PlayerPrefs.GetString("PowerUps").Split(',');
    }
}
