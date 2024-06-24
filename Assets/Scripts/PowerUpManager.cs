using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    /// <summary>
    /// Array containing the powerups that the player has selected.
    /// </summary>
    private string[] _selectedPowerUps;

    void Awake()
    {
        string powerUps = PlayerPrefs.GetString("PowerUps", string.Empty);

        if (string.IsNullOrEmpty(powerUps))
        {
            Debug.LogError("No power-ups found in PlayerPrefs");
            return;
        }

        _selectedPowerUps = powerUps.Split(',');

        if (_selectedPowerUps.Length == 0)
        {
            Debug.LogError("_selectedPowerUps is empty");
            return;
        }
        // this is only here for debugging purposes, remove
        else
        {
            Debug.Log("PowerUps successfully loaded from PlayerPrefs");
            Debug.Log($"Powerups length: {_selectedPowerUps.Length}");
            foreach (var item in _selectedPowerUps)
            {
                Debug.Log("Item: " + item);
            }
        }
    }

    public string[] GetPowerUps()
    {
        return PlayerPrefs.GetString("PowerUps").Split(',');
    }
}
