using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

// This should be used to manage the state of the game
public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
    }

    private void Start() {
        Game_State_Manager.Instance.SetGameState(GameState.MainMenu);
    }

}
