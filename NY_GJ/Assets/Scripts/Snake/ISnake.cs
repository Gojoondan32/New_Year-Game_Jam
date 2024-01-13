using System.Numerics;

public interface ISnake 
{
    public Snake_Part NextPart {get; set;}
    public UnityEngine.Vector3 Position {get;}
    public UnityEngine.Quaternion Rotation {get;}
}
