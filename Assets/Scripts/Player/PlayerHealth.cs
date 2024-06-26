using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public const int MaxHealth = 100;
    private float _currentHealth;
    public float _regenRate; // Health per second
    private bool _hasJuggernaut;

    private PowerUpManager _powerUpManager;
    private string _currentPowerUp;

    private void Start()
    {
        _regenRate = 5;
        _powerUpManager = GetComponent<PowerUpManager>();
        _currentHealth = MaxHealth;
        Debug.Log($"Initial Health: {_currentHealth}");
    }

    private void Update()
    {
        UpdateCurrentPowerUp();
        if (_currentPowerUp == "Regen")
        {
            RegenerateHealth();
        }
    }

    private void UpdateCurrentPowerUp()
    {
        _currentPowerUp = _powerUpManager.GetCurrentPowerUp();
    }

    /// <summary>
    /// Regenerates player health to max health in Update() when regen is activated. 
    /// </summary>
    private void RegenerateHealth()
    {
        _currentHealth += _regenRate * Time.deltaTime;
        if (_currentHealth > MaxHealth)
        {
            _currentHealth = MaxHealth;
        }
        Debug.Log($"Regenerating Health: {_currentHealth}");
    }

    public void EnableJuggernaut()
    {
        _hasJuggernaut = true;
        Debug.Log("Juggernaut mode enabled");
    }

    public void DisableJuggernaut()
    {
        _hasJuggernaut = false;
        Debug.Log("Juggernaut mode disabled");
    }

    public void TakeDamage(int damage)
    {
        if (_hasJuggernaut)
        {
            damage /= 2; // Reduce damage by half
            Debug.Log("Juggernaut mode active: Damage reduced");
        }

        _currentHealth -= damage;
        Debug.Log($"Player took damage: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            // Handle player death
            Debug.Log("Player died");
        }
    }

    public void DisableAllPowerUps()
    {
        DisableJuggernaut();
        Debug.Log("All power-ups disabled");
    }
}
