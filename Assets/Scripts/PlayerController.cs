using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Player speed
    /// </summary>
    [SerializeField] float speed = 5;

    /// <summary>
    /// 2D Vector containing the mouse x and y axes
    /// </summary>
    private Vector3 turn;

    /// <summary>
    /// Checks if the player has the turet powerup
    /// </summary>
    private bool _hasTuret;

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
    /// Reference to the PowerUp Manager script on the Player prefab
    /// </summary>
    private PowerUpManager _powerUpManager;

    private string[] _selectedPowerUps;

    // Start is called before the first frame update
    void Start()
    {
        _powerUpManager = GetComponent<PowerUpManager>();

        if (_powerUpManager != null)
        {
            _selectedPowerUps = _powerUpManager.GetPowerUps();
            
            if (_selectedPowerUps.Contains("Turet"))
            {
                _hasTuret = true;
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

    // Update is called once per frame
    void Update()
    {
        Move();
        Aim();
    }

    private void Move()
    {
        //Receive inputs
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //Move Player
        Vector3 temp = new Vector3(horizontalInput, verticalInput, 0);
        temp = temp.normalized * speed * Time.deltaTime;
        transform.position += temp;
    }

    void Aim()
    {
        //Mouse movement
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        float zRot = Mathf.Atan2(turn.y, turn.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, zRot);
    }
}

