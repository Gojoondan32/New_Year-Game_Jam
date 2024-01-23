using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Snake_Food_Generator : MonoBehaviour
{
    [SerializeField] private Snake_Food _foodPrefab;
    [SerializeField] private Transform _point, _point_2;

    private void Start() => Spawn_Food();

    public void Spawn_Food(){
        float x = Random.Range(_point.position.x, _point_2.position.x);
        float y = Random.Range(_point.position.y, _point_2.position.y);
        Snake_Food snake_Food = Instantiate(_foodPrefab, new Vector3(x, y, 0), Quaternion.identity);
        snake_Food.Init(this);
    }
}
