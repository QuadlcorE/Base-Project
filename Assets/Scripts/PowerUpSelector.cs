using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PowerUpSelector : MonoBehaviour
{
    private List<string> _selectedPowerUps = new List<string>();
    private int maxSelection = 3;

    public List<Button> powerUpButtons;
    public TextMeshProUGUI feedbackText;

    private Scene _currentScene;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

    void Start()
    {
        _currentScene = SceneManager.GetActiveScene();

        foreach (Button button in powerUpButtons)
        {
            button.onClick.AddListener(() => OnPowerUpButtonClicked(button));
        }
    }

    void OnPowerUpButtonClicked(Button button)
    {
        string powerUpName = button.GetComponentInChildren<TextMeshProUGUI>().text;

        if (_selectedPowerUps.Contains(powerUpName))
        {
            _selectedPowerUps.Remove(powerUpName);
            button.GetComponent<Image>().color = Color.white; // Deselect
            feedbackText.text = ""; // Clear feedback
        }
        else
        {
            if (_selectedPowerUps.Count < maxSelection)
            {
                _selectedPowerUps.Add(powerUpName);
                button.GetComponent<Image>().color = Color.green; // Select
                feedbackText.text = ""; // Clear feedback
            }
            else
            {
                feedbackText.text = "You must select " + maxSelection + " power-ups.";
            }
        }

        Debug.Log("Selected Power-Ups: " + string.Join(", ", _selectedPowerUps));
    }

    public void SaveSelectedPowerUps()
    {
        PlayerPrefs.SetString("powerUps", _selectedPowerUps.ToString());
    }

    public void LoadNextScene()
    {
        /// if the current scene is not the powerup slect scene, 
        if (_currentScene.name != "PowerUp-Select-Scene")
        {

            ///     if there is a scene to move to
            ///         move to the next scene
            int nextSceneIndex = _currentScene.buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
        }
        else
        {
            /// else, do these checks
            if (_selectedPowerUps.Count < 3)
            {
                feedbackText.text = "You must select " + maxSelection + " power-ups.";
                return;
            }

            if (feedbackText.text == "")
            {
                SaveSelectedPowerUps();
                int nextSceneIndex = _currentScene.buildIndex + 1;
                SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
            }
        }
    }
}
