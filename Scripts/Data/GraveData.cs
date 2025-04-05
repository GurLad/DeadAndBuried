using Godot;
using System;

public enum GraveType
{
    Single,
    Underground,
    Mass,
    Burn,
    Spaceship,
    EndMarker
}

public class GraveData
{
    public PersonData PersonData { get; set; } = null;
    public int IconID { get; set; }
    public bool IsEmpty => PersonData == null;
    public CoffinType CompatibleTypes { get; set; } = CoffinType.All;

    public bool IsCompatible(CoffinData coffinData)
    {
        return (CompatibleTypes & coffinData.Types) == coffinData.Types;
    }
}
