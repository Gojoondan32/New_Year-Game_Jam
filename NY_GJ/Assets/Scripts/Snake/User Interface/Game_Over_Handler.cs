using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TMPro.EditorUtilities;

public class Game_Over_Handler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TMP_InputField _nameInput;
    private void OnEnable() {
        Debug.Log("Setting score");
        _scoreText.text = UI_Manager.Instance.GetScore().ToString();
    }

    public void GoToMainMenu(){
        Game_State_Manager.Instance.SetGameState(GameState.MainMenu);
    }

}
