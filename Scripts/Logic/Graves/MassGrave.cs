using Godot;
using System;

public partial class MassGrave : AGrave
{
    private GraveType _type;
    public override GraveType Type => _type;

    public override bool CanFill(Coffin coffin)
    {
        return Data.IsCompatible(coffin.Data);
    }

    public override void ForceFill(Coffin coffin)
    {
        // Don't care
    }

    public void SetType(GraveType type)
    {
        _type = type;
    }
}
