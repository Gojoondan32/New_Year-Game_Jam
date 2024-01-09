using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
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
        //double m1 = (1 - (-1)) / (1 - (-1)); // Slope of Line 1
        //double m2 = (-1 - 1) / (1 - (-1)); // Slope of Line 2

        double m1 = (shapeToConnectTo.transform.position.y + shapeToConnectTo.transform.localScale.y / 2) - (shapeToConnectTo.transform.position.y - shapeToConnectTo.transform.localScale.y / 2) / (shapeToConnectTo.transform.position.x + shapeToConnectTo.transform.localScale.x / 2) - (shapeToConnectTo.transform.position.x - shapeToConnectTo.transform.localScale.x / 2);

        double m2 = (shapeToConnectTo.transform.position.y + shapeToConnectTo.transform.localScale.y / 2) - (shapeToConnectTo.transform.position.y - shapeToConnectTo.transform.localScale.y / 2) / (shapeToConnectTo.transform.position.x - shapeToConnectTo.transform.localScale.x / 2) - (shapeToConnectTo.transform.position.x + shapeToConnectTo.transform.localScale.x / 2);

        //! THIS IS BROKEN

        Vector2 line1 = new Vector2((shapeToConnectTo.transform.position.x + shapeToConnectTo.transform.localScale.x) / 2, (shapeToConnectTo.transform.position.y + shapeToConnectTo.transform.localScale.y) / 2);
        Vector2 line2 = new Vector2((shapeToConnectTo.transform.position.x - shapeToConnectTo.transform.localScale.x) / 2, (shapeToConnectTo.transform.position.y - shapeToConnectTo.transform.localScale.y) / 2);

        Vector2 line3 = new Vector2((shapeToConnectTo.transform.position.x - shapeToConnectTo.transform.localScale.x) / 2, (shapeToConnectTo.transform.position.y + shapeToConnectTo.transform.localScale.y) / 2);
        Vector2 line4 = new Vector2((shapeToConnectTo.transform.position.x + shapeToConnectTo.transform.localScale.x) / 2, (shapeToConnectTo.transform.position.y - shapeToConnectTo.transform.localScale.y) / 2);

        Debug.DrawLine(line1, line2, Color.blue, 100f);
        Debug.DrawLine(line3, line4, Color.red, 100f);
        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log($"Mouse Position: {mousePosition}");

        double x = mousePosition.x;
        double y = mousePosition.y;
        Debug.Log($"x: {shapeToConnectTo.transform.position.x}, y: {shapeToConnectTo.transform.position.y}");
        Debug.Log($"x: {x}, y: {y}");
        //double x = 0;
        //double y = -1;

        if (y >= m1 * x && y >= m2 * x)
        {
            // Point is in the first quadrant
            Debug.Log("First Quadrant");
        }
        else if (y < m1 * x && y >= m2 * x)
        {
            // Point is in the fourth quadrant
            Debug.Log("Second Quadrant");
        }
        else if (y < m1 * x && y < m2 * x)
        {
            // Point is in the third quadrant
            Debug.Log("Third Quadrant");
        }
        else if (y >= m1 * x && y < m2 * x)
        {
            // Point is in the second quadrant
            Debug.Log("Fourth Quadrant");
        }
        
        else
        {
            // Point is on one of the lines or at the origin (0,0)
        }


    }
}
