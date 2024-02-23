using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Snake_Generator : MonoBehaviour
{
    public static Snake_Generator Instance { get; private set; }
    [SerializeField] private Snake_Head _snakeHead;
    [SerializeField] private Snake_Part _snakePartPrefab;
    private List<Snake_Data> _snakeData = new List<Snake_Data>();
    private List<GameObject> _snakeParts = new List<GameObject>();
    private ISnake _previousPart; // This is an interface so that the head and the parts can be used interchangeably even though the are different classes
    private int _indexDelay; // This represents the current spacing between the parts
    private const int INDEX_DELAY_AMOUNT = 5; // This represents how much spacing should be between the parts 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Game_State_Manager.Instance.OnGameStateChange += HandleGameStateChange;
    }

    // Used for resetting the game
    private void HandleGameStateChange(GameState gameState){
        if(gameState != GameState.MainMenu) return;

        _snakeData.Add(new Snake_Data(_snakeHead.Position, _snakeHead.Rotation));

        _indexDelay = 0;
        _previousPart = _snakeHead;
        DestroySnakeParts();
    }

    public void GenerateSnakePart(){
        // Need to set the initial position of the parts to behind the previus part
        Snake_Data snake_Data = GetSnakePositionAndRotation(_indexDelay + INDEX_DELAY_AMOUNT);

        Snake_Part newPart = Instantiate(_snakePartPrefab, snake_Data.Position, snake_Data.Rotation);
        _snakeParts.Add(newPart.gameObject); // This is used to keep track of all the parts so that they can be destroyed when the game is reset
        newPart.Init(_indexDelay += INDEX_DELAY_AMOUNT); 
        _previousPart.NextPart = newPart;
        _previousPart = newPart;
    }

    // This gets called every fixed update from the snake head
    public void AddSnakePosition(Vector3 position, Quaternion rotation){
        // If the list is bigger than the index delay + the index delay amount, remove the first element to prevent the list from getting too big
        if(_snakeData.Count >= _indexDelay + INDEX_DELAY_AMOUNT + 1){
            _snakeData.RemoveAt(0);
            Debug.Log("Snake positions" + _snakeData.Count);
        }
        Debug.Log("Index delay: " + _indexDelay);
        _snakeData.Add(new Snake_Data(position, rotation));
    }

    public Snake_Data GetSnakePositionAndRotation(int indexDelay){
        if(_snakeData.Count - (1 + indexDelay) < 0){
            // The list is currently not big enugh to get the position for this part with the correct spacing
            // Add the index delay amount to the index delay to get the position of the previous part
            return _snakeData[^(1 + (indexDelay - INDEX_DELAY_AMOUNT))];
        }

        // The ^ operator is the same as writing _snakeData.Count - 1
        return _snakeData[^(1 + indexDelay)];
    }

    private void DestroySnakeParts(){
        foreach (var part in _snakeParts) Destroy(part);

        _snakeParts.Clear();
    }
}
