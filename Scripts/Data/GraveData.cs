using Godot;
using System;

public class GraveData
{
    public PersonData PersonData = null;
    public int IconID;
    public bool IsEmpty => PersonData == null;
    public CoffinType CompatibleTypes = CoffinType.All;

    public bool IsCompatible(CoffinData coffinData)
    {
        return (CompatibleTypes & coffinData.Types) == coffinData.Types;
    }
}
