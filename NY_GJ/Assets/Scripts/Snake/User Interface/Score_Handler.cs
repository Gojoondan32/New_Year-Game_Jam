using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Handler : MonoBehaviour
{
    private int _score;
    public int Score { get { return _score; }}
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake() {
        _score = 0;
    }

    public void AddScore(int score) {
        _score += score;
        _scoreText.text = _score.ToString();
    }
}
