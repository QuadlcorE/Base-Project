using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            // If the instance is null, find an existing one or create a new one
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    // Create a new GameObject and attach the GameManager script to it
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GameManager>();
                    singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";

                    // Ensure the GameObject persists across scene loads
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;

    private UIManager _uiManager;


    public void PlayerWon()
    {
        _uiManager.ShowWinnerPanel();
    }

    public void PlayerLost()
    {
        _uiManager.ShowLoserPanel();
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        _uiManager = FindObjectOfType<UIManager>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _enemyHealth = FindObjectOfType<EnemyHealth>();
    }

    private void Start()
    {
        ResetWinLosePanels();
        if (_playerHealth != null)
        {
            _playerHealth.OnPlayerLose += HandlePlayerLose;
        }

        if (_enemyHealth != null)
        {
            _enemyHealth.OnEnemyLose += HandleEnemyLose;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void GoToNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    private void OnDisable()
    {
        PlayerPrefs.DeleteAll();
        _playerHealth.OnPlayerLose -= HandlePlayerLose;
        _enemyHealth.OnEnemyLose -= HandleEnemyLose;
    }

    private void ResetWinLosePanels()
    {
        if (_uiManager != null)
        {
            _uiManager.ResetWinLosePanels();
        }
    }

    private void HandlePlayerLose()
    {
        if (_uiManager != null)
        {
            _uiManager.HandlePlayerLose();
        }
    }

    private void HandleEnemyLose()
    {
        if (_uiManager != null)
        {
            _uiManager.HandleEnemyLose();
        }
    }
}
