using UnityEngine;
using TMPro;

public class Game_Over_Handler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private void OnEnable() {
        Debug.Log("Setting score");
        _scoreText.text = UI_Manager.Instance.GetScore().ToString();
    }

}
