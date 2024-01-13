using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake_Part : MonoBehaviour, ISnake
{
    [SerializeField] private Snake_Part _nextPart = null;
    public Snake_Part NextPart { get { return _nextPart; } set { _nextPart = value; } }
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;

    public void Move(Vector3 nextPosition, Quaternion nextRotation)
    {
        _nextPart?.Move(Position, Rotation);

        transform.position = nextPosition;
        transform.rotation = nextRotation;
    }
}
