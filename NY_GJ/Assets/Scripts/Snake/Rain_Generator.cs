using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class Rain_Generator : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _rainTimer;
    [SerializeField] private GameObject _rainPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RainTimer());
    }

    private IEnumerator RainTimer(){
        while (true){
            yield return new WaitForSeconds(_rainTimer);
            GameObject rain = Instantiate(_rainPrefab, GetFallingPoint(), Quaternion.identity);
        }
    }

    private Vector3 GetFallingPoint(){
        Random.InitState(System.DateTime.Now.Millisecond); // Set the seed to the current millisecond
        return new Vector3(Random.Range(_pointA.position.x, _pointB.position.x), Random.Range(_pointA.position.y, _pointB.position.y), 0);
    }
}
