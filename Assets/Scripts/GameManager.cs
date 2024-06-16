using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string[] _selectedPowerUps;

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

        _selectedPowerUps = PlayerPrefs.GetString("powerUps").Split(',');
    }

    void Start()
    {
        if (_selectedPowerUps.Length == 0 || _selectedPowerUps is null)
        {
            Debug.LogError("_selectedPowerUps is null");
        }
        if (_selectedPowerUps.Length > 0)
        {
            Debug.Log("PowerUps successfully loaded from PlayerPrefs ");
            foreach (var item in _selectedPowerUps)
            {
                Debug.Log(item);
            }
        }

    }

    public void GoToNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    }

    private void OnDisable()
    {
        PlayerPrefs.DeleteAll();
    }
}
