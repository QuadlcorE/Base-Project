using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    /// <summary>
    /// Array containing the powerups that the player has selected.
    /// </summary>
    private string[] _selectedPowerUps;

    private void Awake()
    {
        _selectedPowerUps = PlayerPrefs.GetString("powerUps").Split(',');
    }

    void Start()
    {
        if (_selectedPowerUps.Length == 0 || _selectedPowerUps is null)
        {
            Debug.LogError("_selectedPowerUps is null");
            return;
        }
        else if (_selectedPowerUps.Length > 0)
        {
            Debug.Log("PowerUps successfully loaded from PlayerPrefs ");
            foreach (var item in _selectedPowerUps)
            {
                Debug.Log(item);
            }
        }

    }

    public string[] GetPowerUps()
    {
        return _selectedPowerUps;
    }
}
