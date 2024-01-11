using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shape : MonoBehaviour
{
    private Vector2[] directions = new Vector2[4] {Vector2.up, Vector2.right, Vector2.down, Vector2.left};

    [SerializeField] private Shape[] _connctedShapes = new Shape[4];

    private void Awake() {
        // Ensure that the array is empty
        for(int i = 0; i < _connctedShapes.Length; i++){
            _connctedShapes[i] = null;
        }
    }

    // Send in the opposite index to this method
    public void LookForAllEdges(int attachingIndex, Shape shapeToConnectTo){
        // The attaching index represnts the edge this shape is being attached to so we don't need to check that edge again
        for(int i = 0; i < directions.Length; i++){
            if(directions[i] == directions[attachingIndex]) continue; // Skip the edge we are attaching to
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], 0.6f); //! Technically we should be using the size of the shape here
            Debug.DrawRay(transform.position, directions[i] * 0.6f, Color.red, 5f);
            if(hit.collider != null && hit.collider.GetComponent<Shape>() != this){
                Shape shape = hit.collider.GetComponent<Shape>();
                if(shape != null){
                    Debug.Log("We have hit a shape");
                    // We have hit a shape
                    shape.ConnectShape(this, OppositeIndex(i)); // We need to connect to the opposite edge
                    ConnectShape(shape, i);
                }
            }
        }
        ConnectShape(shapeToConnectTo, attachingIndex);
    }

    //! This feels like dupicated code - come back here if there is time
    private int OppositeIndex(int index){
        if(index + 2 >= directions.Length) return (index + 2) - directions.Length;
        else return index + 2;
    }

    // We could have an event here which highlights the edges of the valid shapes
    public void ConnectShape(Shape shape, int index){
        _connctedShapes[index] = shape;
        //AvailableEdges--;
    }

    public void DisconnectShape(int index){
        _connctedShapes[index] = null;
        //AvailableEdges++;
    }

    public bool IsEdgeValid(int index){
        if(_connctedShapes[index] == null) return true;
        else return false;
    }
}
