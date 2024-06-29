using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Turret _turret;
    [SerializeField] private Canon _canon;
    [SerializeField] private DropBox _dropBox;

    private PlayerMovement _playerMovement;
    private PlayerAiming _playerAiming;
    private PlayerHealth _playerHealth;
    private PowerUpManager _powerUpManager;

    private PowerUpManager.Powerups _currentActivePowerUp;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAiming = GetComponent<PlayerAiming>();
        _playerHealth = GetComponent<PlayerHealth>();
        _powerUpManager = GetComponent<PowerUpManager>();
    }

    private void Start()
    {
        if (_powerUpManager != null)
        {
            _powerUpManager.OnPowerUpChanged += HandlePowerUpChanged;
            _currentActivePowerUp = _powerUpManager.GetCurrentActivePowerUp();
        }
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UseActivePowerUp();
        }
    }

    private void FireBullet()
    {
        Instantiate(_bullet, _spawnPoint.position, Quaternion.identity);
    }

    private void UseActivePowerUp()
    {
        switch (_currentActivePowerUp)
        {
            case PowerUpManager.Powerups.Turret:
                Instantiate(_turret, _spawnPoint.position, Quaternion.identity);
                break;

            case PowerUpManager.Powerups.Canon:
                Instantiate(_canon, _spawnPoint.position, Quaternion.identity);
                break;

            case PowerUpManager.Powerups.DropBox:
                Instantiate(_dropBox, _spawnPoint.position, transform.rotation);
                break;

            case PowerUpManager.Powerups.Regen:
                // Do nothing
                break;

            case PowerUpManager.Powerups.Juggernaut:
                // Do nothing
                break;

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
            float _damagePoints = _projectile.GetDamagePoints();
            _playerHealth.TakeDamage(_damagePoints);
        }
    }

    private void HandlePowerUpChanged(PowerUpManager.Powerups newPowerUp)
    {
        switch (newPowerUp)
        {
            case PowerUpManager.Powerups.Regen:
                _currentActivePowerUp = PowerUpManager.Powerups.Regen;
                break;

            case PowerUpManager.Powerups.Juggernaut:
                _currentActivePowerUp = PowerUpManager.Powerups.Juggernaut;
                break;

            case PowerUpManager.Powerups.Turret:
                _currentActivePowerUp = PowerUpManager.Powerups.Turret;
                break;

            case PowerUpManager.Powerups.Canon:
                _currentActivePowerUp = PowerUpManager.Powerups.Canon;
                break;

            case PowerUpManager.Powerups.DropBox:
                _currentActivePowerUp = PowerUpManager.Powerups.DropBox;
                break;

            default:
                Debug.LogWarning("No valid power-up selected");
                break;
        }
    }

    private void OnDisable()
    {
        if (_powerUpManager != null)
        {
            _powerUpManager.OnPowerUpChanged -= HandlePowerUpChanged;
        }
    }
}
