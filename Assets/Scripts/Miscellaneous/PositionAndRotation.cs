using UnityEngine;

public class PositionAndRotation
{
    public Vector3 Position { get; private set; }
    public Quaternion Rotation { get; private set; }

    public PositionAndRotation(Vector3 position, Quaternion rotation)
    {
        this.Position = position;
        this.Rotation = rotation;
    }
}
