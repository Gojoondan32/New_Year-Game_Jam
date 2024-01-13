using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Head : MonoBehaviour, ISnake
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private Snake_Part _nextPart = null;
    public Snake_Part NextPart { get => _nextPart; set => _nextPart = value; }
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;

    void FixedUpdate()
    {
        
        Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Mathf.Abs(_rb.position.x - mousePositon.x) < 0.1f && Mathf.Abs(_rb.position.y - mousePositon.y) < 0.1f)
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        _nextPart?.Move(Position, Rotation); // Send over the old position to the next part
        Move(mousePositon);
        Rotate(mousePositon);
    }

    private void Move(Vector2 mousePositon){
        _rb.velocity = (mousePositon - _rb.position).normalized * _speed * Time.deltaTime;
    }

    private void Rotate(Vector2 mousePositon){
        Vector2 direction = (mousePositon - _rb.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }

}
