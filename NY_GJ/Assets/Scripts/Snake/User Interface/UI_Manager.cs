using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [Header("Class References")]
    [SerializeField] private In_Game_Handler _inGameHandler;

    [Header("Screen References")]
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _leaderboardUI;
    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Game_State_Manager.Instance.OnGameStateChange += HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState gameState) {
        switch (gameState) {
            case GameState.Default:
                break;
            case GameState.MainMenu:
                HideAllUI();
                _mainMenuUI.SetActive(true);
                break;
            case GameState.InGame:
                HideAllUI();
                _inGameUI.SetActive(true);
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                HideAllUI();
                _gameOverUI.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void HideAllUI() {
        _mainMenuUI.SetActive(false);
        _inGameUI.SetActive(false);
        _gameOverUI.SetActive(false);
        _leaderboardUI.SetActive(false);
    }

    #region Handle Score
    public void AddScore(int score){
        Debug.Log(_inGameHandler);
        _inGameHandler.AddScore(score);
    }
    public int GetScore() => _inGameHandler.Score;
    #endregion
}
