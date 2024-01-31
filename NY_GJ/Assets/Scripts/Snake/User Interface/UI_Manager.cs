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
    }

    #region Handle Score
    public void AddScore(int score) => _scoreHandler.AddScore(score);
    public int GetScore() => _scoreHandler.Score;
    #endregion
}
