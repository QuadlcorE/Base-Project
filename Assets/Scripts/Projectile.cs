using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Projectile's speed.
    /// </summary>
    [SerializeField] private int _speed { get; set; }

    ///private PlayerController _playerController;

    /// <summary>
    /// Reference to the Player Controller component.
    /// </summary>
    [SerializeField] private PlayerController _playerController;

    /// <summary>
    /// Projectile's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (_playerController is null)
        {
            Debug.LogError("PlayerController is null");
        }
        _rb.AddForce(new Vector3(_playerController.turn.x, _playerController.turn.y, transform.position.z));
    }

    void Update()
    {
        Debug.Log("turn.x: " + _playerController.turn.x);
        Debug.Log("turn.y: " + _playerController.turn.y);
    }
}
