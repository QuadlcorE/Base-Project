using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public const int MaxHealth = 100;
    private int _health;

    private void Start()
    {
        _health = MaxHealth;
    }

    public void TakeDamage(int damagePoints)
    {
        _health -= damagePoints;
        Debug.Log(_health);
    }

    public int GetHealth()
    {
        return _health;
    }
}
