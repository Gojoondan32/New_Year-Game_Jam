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
    private (List<Vector3> positions, List<Quaternion> rotations) _snakePositions = (new List<Vector3>(), new List<Quaternion>());
    private ISnake _previousPart;
    private int _indexDelay; // This represents the current spacing between the parts
    private int _indexDelayAmount = 4; // This represents how much spacing should be between the parts 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        _snakePositions.positions.Add(_snakeHead.Position); // Add the first position to the list
        _snakePositions.rotations.Add(_snakeHead.Rotation); // Add the first rotation to the list

        _indexDelay = 0;
        _previousPart = _snakeHead;
    }

    public void GenerateSnakePart(){
        // Need to set the initial position of the parts to behind the previus part
        (Vector3 position, Quaternion rotation) = GetSnakePositionAndRotation(_indexDelay + _indexDelayAmount);

        Snake_Part newPart = Instantiate(_snakePartPrefab, position, rotation);
        newPart.Init(_indexDelay += _indexDelayAmount); 
        _previousPart.NextPart = newPart;
        _previousPart = newPart;
    }

    public void AddSnakePosition(Vector3 position, Quaternion rotation){
        // If the list is bigger than the index delay + the index delay amount, remove the first element to prevent the list from getting too big
        if(_snakePositions.positions.Count >= _indexDelay + _indexDelayAmount + 1){
            _snakePositions.positions.RemoveAt(0);
            _snakePositions.rotations.RemoveAt(0);
            Debug.Log("Snake positions" + _snakePositions.positions.Count);
        }
        Debug.Log("Index delay: " + _indexDelay);
        _snakePositions.positions.Add(position);
        _snakePositions.rotations.Add(rotation);
    }

    public (Vector3 position, Quaternion rotation) GetSnakePositionAndRotation(int indexDelay){
        return (_snakePositions.positions[_snakePositions.positions.Count - 1 - indexDelay], _snakePositions.rotations[_snakePositions.rotations.Count - 1 - indexDelay]);
    }
}
