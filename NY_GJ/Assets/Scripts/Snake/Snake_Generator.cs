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
    private int _indexDelay;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        _snakePositions.positions.Add(_snakeHead.Position); // Add the first position to the list
        _snakePositions.rotations.Add(_snakeHead.Rotation); // Add the first rotation to the list

        _indexDelay = 0;
    }

    private void Start() {
        _previousPart = _snakeHead;
        StartCoroutine(GenerateSnake());
    }

    private IEnumerator GenerateSnake(){
        while(true){
            yield return new WaitForSeconds(5f);
            Snake_Part newPart = Instantiate(_snakePartPrefab, _previousPart.Position, _previousPart.Rotation);
            newPart.Init(_indexDelay += 5); //! This number represents the spacing between the parts
            _previousPart.NextPart = newPart;
            _previousPart = newPart;
        }
    }

    public void AddSnakePosition(Vector3 position, Quaternion rotation){
        //! This needs some optimization because it is adding intinite positions to the list
        _snakePositions.positions.Add(position);
        _snakePositions.rotations.Add(rotation);
    }

    public (Vector3 position, Quaternion rotation) GetSnakePositionAndRotation(int indexDelay){
        return (_snakePositions.positions[_snakePositions.positions.Count - 1 - indexDelay], _snakePositions.rotations[_snakePositions.rotations.Count - 1 - indexDelay]);
    }
}
