using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Rain_Object : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = Vector2.down * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        
    }
}
