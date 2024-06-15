using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PowerUpSelector : MonoBehaviour
{
    public List<Button> powerUpButtons;
    private List<string> selectedPowerUps = new List<string>();
    private int maxSelection = 3;
    public TextMeshProUGUI feedbackText;
    Scene _currentScene;

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

        if (selectedPowerUps.Contains(powerUpName))
        {
            selectedPowerUps.Remove(powerUpName);
            button.GetComponent<Image>().color = Color.white; // Deselect
            feedbackText.text = ""; // Clear feedback
        }
        else
        {
            if (selectedPowerUps.Count < maxSelection)
            {
                selectedPowerUps.Add(powerUpName);
                button.GetComponent<Image>().color = Color.green; // Select
                feedbackText.text = ""; // Clear feedback
            }
            else
            {
                feedbackText.text = "You can only select up to " + maxSelection + " power-ups.";
            }
        }

        Debug.Log("Selected Power-Ups: " + string.Join(", ", selectedPowerUps));
    }

    public void LoadNextScene()
    {
        if (feedbackText.text == "")
        {
            int nextSceneIndex = (_currentScene.buildIndex + 1) % SceneManager.sceneCount;
            SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
        }
    }
}
