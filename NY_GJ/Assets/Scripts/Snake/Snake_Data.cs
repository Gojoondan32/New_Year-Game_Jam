using UnityEngine;

public class Snake_Data 
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Snake_Data(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Rotation = rotation;
    }
}
