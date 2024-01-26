using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void UpdateScore(int score) {
        _scoreText.text = score.ToString();
    }
}
