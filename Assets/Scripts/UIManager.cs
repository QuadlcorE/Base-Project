using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _winnerPanel;
    [SerializeField] private GameObject _loserPanel;

    private void Awake()
    {
        ResetWinLosePanels();
    }

    public void ShowWinnerPanel()
    {
        _winnerPanel.SetActive(true);
    }

    public void ShowLoserPanel()
    {
        _loserPanel.SetActive(true);
    }

    public void ResetWinLosePanels()
    {
        _loserPanel.SetActive(false);
        _winnerPanel.SetActive(false);
    }

    public void HandlePlayerLose()
    {
        _loserPanel.SetActive(true);
        _winnerPanel.SetActive(false);
    }

    public void HandleEnemyLose()
    {
        _winnerPanel.SetActive(true);
        _loserPanel.SetActive(false);
    }

    public void OnRestartButtonClicked()
    {
        GameManager _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager != null)
        {
            _gameManager.RestartGame();
        }
    }

    public void OnMainMenuButtonClicked()
    {
        GameManager _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager != null)
        {
            _gameManager.LoadMainMenu();
        }
    }

    private void OnDisable()
    {
        ResetWinLosePanels();
    }
}
