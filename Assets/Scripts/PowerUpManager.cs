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

    [SerializeField] private TextMeshProUGUI _currentPowerUpText;


    void Awake()
    {
        _selectedPowerUps = GetSelectedPowerUps();

        //Set default _currentActivePowerUp to _selectedPowerUps[0]
        _currentActivePowerUp = (Powerups)Enum.Parse(typeof(Powerups), _selectedPowerUps[0]);
    }

    private void Start()
    {
        _currentPowerUpText.text = _selectedPowerUps[0];
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
        if (Enum.TryParse(powerUp, out Powerups newPowerUp))
        {
            _currentActivePowerUp = newPowerUp;
            _currentPowerUpText.text = powerUp.ToString();
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
        return PlayerPrefs.GetString("PowerUps").Split(',').ToList();
    }
}
