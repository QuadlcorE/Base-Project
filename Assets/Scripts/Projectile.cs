using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Projectile's speed.
    /// </summary>
    [SerializeField] private float _speed = 1000f;

    /// <summary>
    /// Projectile's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// Reference to the Player Controller component.
    /// </summary>
    private PlayerController _playerController;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        // Ensure the Rigidbody2D settings for continuous movement
        _rb.gravityScale = 0;
        _rb.drag = 0;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();

        if (_playerController == null)
        {
            Debug.LogError("PlayerController is null");
            return;
        }

        // Set initial velocity based on player direction
        Vector2 initialVelocity = new Vector2(_playerController.turn.x, _playerController.turn.y).normalized * _speed;
        _rb.velocity = initialVelocity;
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
