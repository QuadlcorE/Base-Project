using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Maximum possible health for player
    /// </summary>
    public const int MaxHealth = 100;

    /// <summary>
    /// Player's current health
    /// </summary>
    private int _health;

    /// <summary>
    /// Player speed
    /// </summary>
    [SerializeField] private float _speed = 5;

    /// <summary>
    /// 2D Vector containing the mouse x and y axes
    /// </summary>
    public Vector3 turn { get; private set; }

    /// <summary>
    /// Checks if the player has the turret powerup
    /// </summary>
    private bool _hasTurret;

    /// <summary>
    /// Checks if the player has the juggernaut powerup
    /// </summary>
    private bool _hasJuggernaut;

    /// <summary>
    /// Checks if the player has the canon powerup
    /// </summary>
    private bool _hasCanon;

    /// <summary>
    /// Checks if the player has quick regenerative powers
    /// </summary>
    private bool _hasRegen;

    /// <summary>
    /// Checks if the player has the dropbox powerup
    /// </summary>
    private bool _hasDropBox;

    /// <summary>
    /// Reference to the PowerUp Manager script.
    /// </summary>
    [SerializeField] private PowerUpManager _powerUpManager;

    private string[] _selectedPowerUps;

    [SerializeField] private Projectile _projectile;

    private void Start()
    {
        _health = MaxHealth;

        if (_powerUpManager != null)
        {
            _selectedPowerUps = _powerUpManager.GetPowerUps();

            if (_selectedPowerUps.Contains("Turret"))
            {
                _hasTurret = true;
                Debug.Log($"Has turret: {_hasTurret}");
            }

            if (_selectedPowerUps.Contains("Juggernaut"))
            {
                _hasJuggernaut = true;
                Debug.Log($"Has juggernaut: {_hasJuggernaut}");
            }

            if (_selectedPowerUps.Contains("Canon"))
            {
                _hasCanon = true;
                Debug.Log($"Has canon: {_hasCanon}");
            }

            if (_selectedPowerUps.Contains("Regen"))
            {
                _hasRegen = true;
                Debug.Log($"Has regen: {_hasRegen}");
            }

            if (_selectedPowerUps.Contains("Drop Box"))
            {
                _hasDropBox = true;
                Debug.Log($"Has drop box: {_hasDropBox}");
            }
        }
        else
        {
            Debug.LogError("Powerup Manager script is null");
        }
    }

    private void Update()
    {
        Move();
        Aim();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }

    private void Move()
    {
        // Receive inputs
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Move Player
        Vector3 temp = new Vector3(horizontalInput, verticalInput, 0);
        temp = temp.normalized * _speed * Time.deltaTime;
        transform.position += temp;
    }

    private void Aim()
    {
        // Get mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Calculate direction from player to mouse
        turn = (mousePosition - transform.position).normalized;

        // Calculate the rotation angle
        float zRot = Mathf.Atan2(turn.y, turn.x) * Mathf.Rad2Deg;

        // Apply rotation
        transform.localRotation = Quaternion.Euler(0, 0, zRot);
    }

    private void SpawnProjectile()
    {
        Instantiate(_projectile, transform.position, Quaternion.identity);
    }
}
