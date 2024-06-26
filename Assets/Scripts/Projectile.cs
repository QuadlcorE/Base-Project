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

    private Vector2 _direction;

    private void Start()
    {
        _powerUpManager = GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>();
        InitializeRigidbody2D();
        FindPlayerController();
        SetInitialVelocity();
    }

    public void Shoot(Vector2 shootDirection)
    {
        _direction = shootDirection;
        Debug.Log(_rb.velocity);
        _rb.velocity = _direction * _speed * Time.deltaTime;
        Debug.Log(_rb.velocity);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        var firstContact = collision.GetContact(0);
        Vector2 newVelocity = Vector2.Reflect(_direction, firstContact.normal);
        Shoot(newVelocity.normalized);
        //}
        //else
        //{
        //Destroy(gameObject);
        //}
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
        _direction = new Vector2(_playerAiming.turn.x, _playerAiming.turn.y);
        Vector2 initialVelocity = _direction.normalized * _speed * Time.deltaTime;
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
