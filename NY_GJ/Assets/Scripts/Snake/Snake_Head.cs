using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Snake_Head : MonoBehaviour, ISnake
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    private Snake_Part _nextPart = null;
    public Snake_Part NextPart { get => _nextPart; set => _nextPart = value; }
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;

    void FixedUpdate()
    {
        if(Game_State_Manager.Instance.CurrentGameState != GameState.InGame) return;


        Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Mathf.Abs(_rb.position.x - mousePositon.x) < 0.1f && Mathf.Abs(_rb.position.y - mousePositon.y) < 0.1f)
        {
            //_rb.velocity = Vector2.zero;
            //return;
        }



        _nextPart?.Move(Position, transform.up, Rotation); // Send over the old position to the next part
        Move(mousePositon);
        Rotate(mousePositon);
    }

    private void Move(Vector2 mousePositon){
        //_rb.velocity = (mousePositon - _rb.position).normalized * _speed * Time.deltaTime;
        _rb.velocity = transform.up * _speed * Time.deltaTime;
        Snake_Generator.Instance.AddSnakePosition(transform.position, transform.rotation);
    }

    private void Rotate(Vector2 mousePositon){
        Vector2 direction = (mousePositon - _rb.position).normalized;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, direction), _rotationSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.TryGetComponent<Snake_Part>(out _)){
            // This is game over
            Debug.Log("Game Over");
            Game_State_Manager.Instance.SetGameState(GameState.GameOver);
        }
    }

}
