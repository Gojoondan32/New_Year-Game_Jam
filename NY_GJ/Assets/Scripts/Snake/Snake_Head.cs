using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Head : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    //private List<Transform> _positions = new List<Transform>();
    //public List<Transform> Positions {get => _positions; private set => _positions = value;}

    private Snake_Part _nextPart = null;

    void FixedUpdate()
    {
        Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Mathf.Abs(_rb.position.x - mousePositon.x) < 0.1f && Mathf.Abs(_rb.position.y - mousePositon.y) < 0.1f)
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        Move(mousePositon);
        Rotate(mousePositon);
        //_positions.Add(transform);
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
