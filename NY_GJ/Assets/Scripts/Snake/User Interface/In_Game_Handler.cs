using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class In_Game_Handler : MonoBehaviour
{
    private int _score;
    public int Score { get { return _score; }}
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake() {
        Game_State_Manager.Instance.OnGameStateChange += HandleGameStateChange;
    }

    // Used for resetting the score when the game starts
    private void HandleGameStateChange(GameState gameState) {
        if (gameState != GameState.MainMenu) return;
        _score = 0;
        _scoreText.text = _score.ToString();
    }

    public void AddScore(int score) {
        _score += score;
        _scoreText.text = _score.ToString();
    }
}
