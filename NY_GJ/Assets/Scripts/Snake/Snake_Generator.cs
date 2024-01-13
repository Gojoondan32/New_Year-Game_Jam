using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Snake_Generator : MonoBehaviour
{
    [SerializeField] private Snake_Head _snakeHead;
    [SerializeField] private Snake_Part _snakePartPrefab;
    private ISnake _previousPart;

    private void Start() {
        _previousPart = _snakeHead;
        StartCoroutine(GenerateSnake());
    }

    private IEnumerator GenerateSnake(){
        while(true){
            yield return new WaitForSeconds(5f);
            Snake_Part newPart = Instantiate(_snakePartPrefab, _previousPart.Position, _previousPart.Rotation);
            _previousPart.NextPart = newPart;
            _previousPart = newPart;
        }
    }
}
