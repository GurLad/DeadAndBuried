using Godot;
using System;

public class CemeteryData
{
    public GraveType Type;
    public Vector2I Location;
    public Texture2D Background;

    public CemeteryData(GraveType type, Vector2I location)
    {
        Type = type;
        Location = location;
    }
}
