using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : NetworkBehaviour
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
    /// The point from which all bullets and projectiles spawn.
    /// </summary>
    [SerializeField] private Transform _spawnPoint;

    /// <summary>
    /// Reference to the PowerUp Manager script.
    /// </summary>
    [SerializeField] private PowerUpManager _powerUpManager;

    /// <summary>
    /// Powerups selected by the player.
    /// </summary>
    private string[] _selectedPowerUps;

    /// <summary>
    /// Network character controller.
    /// </summary>
    private NetworkCharacterController _cc;

    /// <summary>
    /// Dropbox prefab.
    /// </summary>
    [SerializeField] private DropBox _dropBox;

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
    /// True if player selected turret card.
    /// </summary>
    private bool _hasTurret;

    /// <summary>
    /// True if player selected juggernaut card.
    /// </summary>
    private bool _hasJuggernaut;

    /// <summary>
    /// True if player selected canon card.
    /// </summary>
    private bool _hasCanon;

    /// <summary>
    /// True if player selected regen card.
    /// </summary>
    private bool _hasRegen;

    /// <summary>
    /// True if player selected drop box card.
    /// </summary>
    private bool _hasDropBox;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(5 * data.direction * Runner.DeltaTime);
        }
    }

    private void Start()
    {
        _health = MaxHealth;

        if (_powerUpManager != null)
        {
            _selectedPowerUps = _powerUpManager.GetPowerUps();

            if (_selectedPowerUps.Contains("Turret"))
            {
                _hasTurret = true;
            }

            if (_selectedPowerUps.Contains("Juggernaut"))
            {
                _hasJuggernaut = true;
            }

            if (_selectedPowerUps.Contains("Canon"))
            {
                _hasCanon = true;
            }

            if (_selectedPowerUps.Contains("Regen"))
            {
                _hasRegen = true;
            }

            if (_selectedPowerUps.Contains("Drop Box"))
            {
                _hasDropBox = true;
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
            Instantiate(_bullet, _spawnPoint.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_hasTurret)
            {
                Instantiate(_turret, _spawnPoint.position, Quaternion.identity);

            }
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (_hasCanon)
            {

                Instantiate(_canon, _spawnPoint.position, Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_hasDropBox)
            {
                Instantiate(_dropBox, _spawnPoint.position, transform.rotation);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile _projectile = collision.gameObject.GetComponent<Projectile>();
        if (_projectile != null)
        {
            TakeDamage(_projectile.GetDamagePoints());
            Debug.Log(_health);
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

    private void TakeDamage(int damagePoints)
    {
        _health -= damagePoints;
    }
}
