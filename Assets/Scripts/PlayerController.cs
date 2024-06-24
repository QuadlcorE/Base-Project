using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 2D Vector containing the mouse x and y axes
    /// </summary>
    public Vector3 turn { get; private set; }

    /// <summary>
    /// The point from which all bullets and projectiles spawn.
    /// </summary>
    [SerializeField] private Transform _spawnPoint;

    /// <summary>
    /// Default Bullet prefab.
    /// </summary>
    [SerializeField] private Bullet _bullet;

    /// <summary>
    /// Turret prefab.
    /// </summary>
    [SerializeField] private Turret _turret;

    /// <summary>
    /// Canon prefab.
    /// </summary>
    [SerializeField] private Canon _canon;

    /// <summary>
    /// Dropbox prefab.
    /// </summary>
    [SerializeField] private DropBox _dropBox;

    /// <summary>
    /// 
    /// </summary>
    private PlayerMovement _playerMovement;
    
    /// <summary>
    /// 
    /// </summary>
    private PlayerAiming _playerAiming;
    
    /// <summary>
    /// 
    /// </summary>
    private PlayerHealth _playerHealth;
    
    /// <summary>
    /// 
    /// </summary>
    private PowerUpManager _powerUpManager;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAiming = GetComponent<PlayerAiming>();
        _playerHealth = GetComponent<PlayerHealth>();
        _powerUpManager = GetComponent<PowerUpManager>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        _playerMovement.MoveLocal(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _playerAiming.Aim();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
        
        // Use the active power-up from the PowerUpManager
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            FireActivePowerUp();
        }
    }

    private void FireBullet()
    {
        Instantiate(_bullet, _spawnPoint.position, Quaternion.identity);
    }


    private void FireActivePowerUp()
    {
        string currentPowerUp = _powerUpManager.GetCurrentPowerUp();

        switch (currentPowerUp)
        {
            case "Turret":
                Instantiate(_turret, _spawnPoint.position, Quaternion.identity);
                break;

            case "Canon":
                Instantiate(_canon, _spawnPoint.position, Quaternion.identity);
                break;

            case "Drop Box":
                Instantiate(_dropBox, _spawnPoint.position, transform.rotation);
                break;

            // Add cases for other power-ups

            default:
                Debug.LogWarning("No valid power-up selected");
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile _projectile = collision.gameObject.GetComponent<Projectile>();
        if (_projectile != null)
        {
            _playerHealth.TakeDamage(_projectile.GetDamagePoints());
        }
    }
}
