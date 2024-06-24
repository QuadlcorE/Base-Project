using System.Linq;
using UnityEngine;

public class PlayerPowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerUpManager _powerUpManager;

    private string[] _selectedPowerUps;
    public bool HasTurret { get; private set; }
    public bool HasJuggernaut { get; private set; }
    public bool HasCanon { get; private set; }
    public bool HasRegen { get; private set; }
    public bool HasDropBox { get; private set; }

    private void Awake()
    {

        if (_powerUpManager != null)
        {
            _selectedPowerUps = _powerUpManager.GetPowerUps();

            HasTurret = _selectedPowerUps.Contains("Turret");
            HasJuggernaut = _selectedPowerUps.Contains("Juggernaut");
            HasCanon = _selectedPowerUps.Contains("Canon");
            HasRegen = _selectedPowerUps.Contains("Regen");
            HasDropBox = _selectedPowerUps.Contains("Drop Box");
        }
        else
        {
            Debug.LogError("Powerup Manager script is null");
        }
    }
}
