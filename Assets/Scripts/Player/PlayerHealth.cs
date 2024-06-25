using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public const int MaxHealth = 100;
    private int _currentHealth;
    private bool _isRegenerating;
    private float _regenRate = 5f; // Health per second
    private bool _hasJuggernaut;

    private void Start()
    {
        _currentHealth = MaxHealth;
        Debug.Log($"Initial Health: {_currentHealth}");
    }

    private void Update()
    {
        if (_isRegenerating)
        {
            RegenerateHealth();
        }
    }

    private void RegenerateHealth()
    {
        if (_currentHealth < MaxHealth)
        {
            _currentHealth += (int)(_regenRate * Time.deltaTime);
            _currentHealth = Mathf.Min(_currentHealth, MaxHealth);
            Debug.Log($"Health regenerating: {_currentHealth}");
        }
    }

    public void EnableRegeneration()
    {
        _isRegenerating = true;
        Debug.Log("Health regeneration enabled");
    }

    public void DisableRegeneration()
    {
        _isRegenerating = false;
        Debug.Log("Health regeneration disabled");
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
        DisableRegeneration();
        DisableJuggernaut();
        Debug.Log("All power-ups disabled");
    }
}
