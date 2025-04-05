using Godot;
using System;

public class CemeteryData
{
    public GraveType Type;
    public Vector2I Location;
    public float YPositionMod;
    public Texture2D Background;

    public CemeteryData(GraveType type, Vector2I location, float yPositionMod)
    {
        Type = type;
        Location = location;
        YPositionMod = yPositionMod;
    }
}
