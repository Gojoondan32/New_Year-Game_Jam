using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake_Food : MonoBehaviour
{
    private Snake_Food_Generator _generator;
    private int _scoreValue = 1;

    // This script needs a reference to the generator so it can call the spawn food method when it gets eaten
    public void Init(Snake_Food_Generator generator){
        _generator = generator;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent<Snake_Head>(out _)){
            _generator.Spawn_Food();
            Snake_Generator.Instance.GenerateSnakePart();
            Game_Manager.Instance.AddScore(_scoreValue);
            Destroy(gameObject);
        }
    }
}
