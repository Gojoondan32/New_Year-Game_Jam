using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //TestAngle(mousePositon);
        Move(mousePositon);
        Rotate(mousePositon);
    }

    private void Move(Vector2 mousePositon){
        // 
        if(Mathf.Abs(rb.position.x - mousePositon.x) < 0.1f && Mathf.Abs(rb.position.y - mousePositon.y) < 0.1f){
            rb.velocity = Vector2.zero;
            return;
        }
        
        rb.velocity = (mousePositon - rb.position).normalized * speed * Time.deltaTime;
    }
    
    private void Rotate(Vector2 mousePosition){
        Vector2 rotationDirection = mousePosition - rb.position;
        float rotation = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg - 90f;
        rb.transform.eulerAngles = Vector3.forward * rotation;
    }

    private void TestAngle(Vector2 mousePosition){
        Vector2 up = new Vector2(0, 1);
        Debug.DrawRay(transform.position, mousePosition, Color.red); // The vector which points to the mouse position
        Debug.DrawRay(Vector2.zero, up, Color.blue); // Represents the up vector

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) - Mathf.Atan2(rb.position.y, rb.position.x) * Mathf.Rad2Deg;
        if(angle < 0) angle += 360f;
        Debug.Log(angle);
    }
}
