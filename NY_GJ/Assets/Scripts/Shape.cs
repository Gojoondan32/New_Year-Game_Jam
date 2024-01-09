using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private int _availableEdges = 4;
    public int AvailableEdges { get { return _availableEdges; } private set { _availableEdges = value; } }

    private Shape[] _connctedShapes = new Shape[4];

    private void Awake() {
        // Ensure that the array is empty
        for(int i = 0; i < _connctedShapes.Length; i++){
            _connctedShapes[i] = null;
        }
    }

    // We could have an event here which highlights the edges of the valid shapes
    public void ConnectShape(Shape shape, int index){
        _connctedShapes[index] = shape;
        AvailableEdges--;
    }

    public void DisconnectShape(int index){
        _connctedShapes[index] = null;
        AvailableEdges++;
    }
}
