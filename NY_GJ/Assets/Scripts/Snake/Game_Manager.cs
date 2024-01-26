using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance { get; private set; }
    [SerializeField] private UI_Manager _uiManager;
    private int score;
    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        score = 0;
    }


    public void AddScore(int value) {
        score += value;
        _uiManager.UpdateScore(score);
    }

    public int GetScore() => score;
}
