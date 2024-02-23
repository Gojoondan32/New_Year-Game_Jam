using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Snake_Part : MonoBehaviour, ISnake
{
    private Snake_Part _nextPart = null;
    public Snake_Part NextPart { get { return _nextPart; } set { _nextPart = value; } }
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
    private int _indexDelay;

    public void Init(int indexDelay) => _indexDelay = indexDelay;

    public void Move(Vector3 nextPosition, Vector3 localUp, Quaternion nextRotation)
    {
        _nextPart?.Move(Position, transform.up, Rotation);


        Snake_Data snake_Data = Snake_Generator.Instance.GetSnakePositionAndRotation(_indexDelay);
        transform.position = snake_Data.Position;
        transform.rotation = snake_Data.Rotation;
    }
}
