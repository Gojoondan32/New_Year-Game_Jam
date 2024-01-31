using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [SerializeField] private Score_Handler _scoreHandler;

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
                break;
            case GameState.InGame:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
    }

    #region Handle Score
    public void AddScore(int score) => _scoreHandler.AddScore(score);
    public int GetScore() => _scoreHandler.Score;
    #endregion
}
