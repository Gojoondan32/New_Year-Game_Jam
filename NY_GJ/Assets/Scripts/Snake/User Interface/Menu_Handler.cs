using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Handler : MonoBehaviour
{
    // Called from the Start Game button
    public void StartGame(){
        Game_State_Manager.Instance.SetGameState(GameState.InGame);
    }
}
