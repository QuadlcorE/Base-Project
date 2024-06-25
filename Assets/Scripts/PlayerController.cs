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
        _playerHealth.DisableAllPowerUps();

        string currentPowerUp = _powerUpManager.GetCurrentPowerUp();
        Debug.Log($"Activating power-up: {currentPowerUp}");

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

            case "Regen":
                _playerHealth.EnableRegeneration();
                break;

            case "Juggernaut":
                _playerHealth.EnableJuggernaut();
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
            string currentPowerUp = _powerUpManager.GetCurrentPowerUp();
            _playerHealth.TakeDamage(_projectile.GetDamagePoints());
        }
    }
}
