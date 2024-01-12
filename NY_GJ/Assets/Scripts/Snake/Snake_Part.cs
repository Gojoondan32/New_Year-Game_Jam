using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake_Part : MonoBehaviour
{
    private Snake_Part _nextPart = null;
    public Snake_Part NextPart {private get {return _nextPart;} set {_nextPart = value;}}

    public void Move(Vector2 position){
        
    }
}
