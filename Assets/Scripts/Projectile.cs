using System;
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
    protected int damagePoints;

    /// <summary>
    /// Projectile's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// Reference to the Player Controller component.
    /// </summary>
    private PlayerController _playerController;

    private void Start()
    {
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
        _playerController = FindObjectOfType<PlayerController>();
        if (_playerController == null)
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
        Vector2 initialVelocity = new Vector2(_playerController.turn.x, _playerController.turn.y).normalized * _speed * Time.deltaTime;
        _rb.velocity = initialVelocity;
    }

    public int GetDamagePoints()
    {
        return damagePoints;
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect the projectile's velocity when it hits a wall
        Vector2 normal = collision.GetContact(0).normal;
        _rb.velocity = Vector2.Reflect(_rb.velocity, normal).normalized * _speed;
    }
    */


}
