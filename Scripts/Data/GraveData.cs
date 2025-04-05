using Godot;
using System;

public enum GraveType
{
    None = 0,
    Single = 1,
    Underground = 2,
    Mass = 3,
    Burn = 4,
    Spaceship = 5,
    EndMarker = 6,
    Filled = 64
}

public class GraveData
{
    public PersonData PersonData { get; set; } = null;
    public int FilledIconID { get; set; }
    public bool IsEmpty => PersonData == null;
    public CoffinType CompatibleTypes { get; set; } = CoffinType.All;
    public int ScoreMultiplier { get; set; } = 1;

    public bool IsCompatible(CoffinData coffinData)
    {
        return (CompatibleTypes & coffinData.Types) == coffinData.Types;
    }
}
