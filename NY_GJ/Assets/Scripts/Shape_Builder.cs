using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class Shape_Builder : MonoBehaviour
{
    [SerializeField] private Shape _playerShape; //! This will need to change at runtime later
    [SerializeField] private Shape _shapeWaitingToConnect;
    private bool _connectingShape = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This is just for testing, the input code will be in the Player.cs script
        if(Input.GetMouseButtonDown(1)){
            
            if(_connectingShape){
                Shape shapeToConnectTo = Valid_Shape();
                if(shapeToConnectTo == null) return;
                Debug.Log("Shape to connect to: " + shapeToConnectTo.name);
                TryAttach_Shape(_shapeWaitingToConnect, shapeToConnectTo);
            }
            else{
                Shape shapeToConnectTo = Valid_Shape();
                if (shapeToConnectTo == null) return;
                else{
                    _shapeWaitingToConnect = shapeToConnectTo;
                    _connectingShape = true;
                }
            }
        }
    }

    private Shape Valid_Shape(){
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        // Clicked on nothing or clicked on something that isn't a shape
        if (hit.collider == null || !hit.collider.TryGetComponent<Shape>(out Shape shape)) return null;
        else return shape;
    }

    private void TryAttach_Shape(Shape shapeWaitingToConnect, Shape shapeToConnectTo){   
        // This makes the code below easier to read by reducing the complexity of the variable names
        float shapeX = shapeToConnectTo.transform.position.x;
        float shapeY = shapeToConnectTo.transform.position.y;
        float shapeWidth = shapeToConnectTo.transform.localScale.x;
        float shapeHeight = shapeToConnectTo.transform.localScale.y;

        // These represent the coordinates of the two lines that make up the shape
        Vector2 line1Coord1 = new Vector2(shapeX + (shapeWidth / 2), shapeY + (shapeHeight / 2));
        Vector2 line1Coor2 = new Vector2(shapeX - (shapeWidth / 2), shapeY - (shapeHeight / 2));
        Vector2 line2Coord1 = new Vector2(shapeX - (shapeWidth / 2), shapeY + (shapeHeight / 2));
        Vector2 line2Coord2 = new Vector2(shapeX + (shapeWidth / 2), shapeY - (shapeHeight / 2));

        Debug.DrawLine(line1Coord1, line1Coor2, Color.blue, 100f);
        Debug.DrawLine(line2Coord1, line2Coord2, Color.red, 100f);
        Debug.Log("Line 1: " + line1Coord1 + ", " + line1Coor2);
        Debug.Log("Line 2: " + line2Coord1 + ", " + line2Coord2);
        
        double m1 = (line1Coord1.y - line1Coor2.y) / (line1Coord1.x - line1Coor2.x); // Slope of Line 1
        double m2 = (line2Coord1.y - line2Coord2.y) / (line2Coord1.x - line2Coord2.x); // Slope of Line 2

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Similar to above, this makes the code below easier to read
        double x = mousePosition.x;
        double y = mousePosition.y;

        if (y >= m1 * (x - line1Coord1.x) + line1Coord1.y && y >= m2 * (x - line2Coord1.x) + line2Coord1.y){
            Debug.Log("Point is in the first quadrant");
            if(shapeToConnectTo.IsEdgeValid(0)){
                // We can connect the shape here
                AttachShape(shapeWaitingToConnect, shapeToConnectTo, 0);
            }
                
        }
        else if (y < m1 * (x - line1Coord1.x) + line1Coord1.y && y >= m2 * (x - line2Coord1.x) + line2Coord1.y){
            Debug.Log("Point is in the second quadrant");
            if (shapeToConnectTo.IsEdgeValid(1)){
                // We can connect the shape here
                AttachShape(shapeWaitingToConnect, shapeToConnectTo, 1);
            }
        }
        else if (y < m1 * (x - line1Coord1.x) + line1Coord1.y && y < m2 * (x - line2Coord1.x) + line2Coord1.y){
            Debug.Log("Point is in the third quadrant");
            if (shapeToConnectTo.IsEdgeValid(2)){
                // We can connect the shape here
                AttachShape(shapeWaitingToConnect, shapeToConnectTo, 2);
            }
        }
        else if (y >= m1 * (x - line1Coord1.x) + line1Coord1.y && y < m2 * (x - line2Coord1.x) + line2Coord1.y){
            Debug.Log("Point is in the fourth quadrant");
            if (shapeToConnectTo.IsEdgeValid(3)){
                // We can connect the shape here
                AttachShape(shapeWaitingToConnect, shapeToConnectTo, 3);
            }
        }
        else
        {
            // Point is on one of the lines or at the origin (0,0)
        }
    }

    private void AttachShape(Shape shapeWaitingToConnect, Shape shapeToConnectTo, int index){
        // We only need to call IndexConverter once this way
        (int index, Vector3 position) indexConverter = IndexConverter(index);
        shapeWaitingToConnect.ConnectShape(shapeToConnectTo, indexConverter.index); 
        shapeWaitingToConnect.transform.position = shapeToConnectTo.transform.position + indexConverter.position;

        shapeToConnectTo.ConnectShape(shapeWaitingToConnect, index);
        _connectingShape = false;
        _shapeWaitingToConnect = null;
    }

    private (int index, Vector3 position) IndexConverter(int index){
        return index switch{
            0 => (2, new Vector3(0, 1f, 0)),
            1 => (3, new Vector3(1f, 0, 0)),
            2 => (0, new Vector3(0, -1f, 0)),
            3 => (1, new Vector3(-1f, 0, 0)),
            _ => (-1, new Vector3(0, 0, 0)),
        };
    }
}
