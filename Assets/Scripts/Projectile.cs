using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Projectile's speed.
    /// </summary>
    protected float _speed;

    /// <summary>
    /// Damage points.
    /// </summary>
    protected float damagePoints;

    /// <summary>
    /// Projectile's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// Reference to the Player Controller component.
    /// </summary>
    private PlayerAiming _playerAiming;

    private PowerUpManager _powerUpManager;

    private void Start()
    {
        _powerUpManager = GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>();
        InitializeRigidbody2D();
        FindPlayerController();
        SetInitialVelocity();
    }


    /// <summary>
    /// Ensure the Rigidbody2D settings for continuous movement
    /// </summary>
    private void InitializeRigidbody2D()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.gravityScale = 0;
        _rb.drag = 0;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    /// <summary>
    /// Find and initialize PlayerController component 
    /// </summary>
    private void FindPlayerController()
    {
        _playerAiming = FindObjectOfType<PlayerAiming>();
        if (_playerAiming == null)
        {
            Debug.LogError("PlayerController is null");
            return;
        }
    }

    /// <summary>
    /// Set initial velocity of the projectile based on player direction
    /// </summary>
    private void SetInitialVelocity()
    {
        Vector2 initialVelocity = new Vector2(_playerAiming.turn.x, _playerAiming.turn.y).normalized * _speed * Time.deltaTime;
        _rb.velocity = initialVelocity;
    }

    public float GetDamagePoints()
    {
        if (_powerUpManager != null)
        {
            PowerUpManager.Powerups currentPowerUp = _powerUpManager.GetCurrentActivePowerUp();
            return (currentPowerUp == PowerUpManager.Powerups.Juggernaut) ? damagePoints / 2 : damagePoints;
        }
        else
        {
            Debug.LogError("_powerUpManager is null");
            return 0;
        }
    }
}
