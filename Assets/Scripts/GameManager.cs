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

    [SerializeField] private GameObject _loserPanel;
    [SerializeField] private GameObject _winnerPanel;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DebugLoss();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            DebugWin();
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

    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    private void OnDisable()
    {
        PlayerPrefs.DeleteAll();
    }


    ///
    // handle loss
    private void DebugLoss()
    {
        _loserPanel.SetActive(true);
        _winnerPanel.SetActive(false);
    }

    private void DebugWin()
    {
        _winnerPanel.SetActive(true);
        _loserPanel.SetActive(false);
    }
}
