using Godot;
using System;

public partial class MassGrave : AGrave
{
    public override bool CanFill(Coffin coffin)
    {
        return Data.IsCompatible(coffin.Data);
    }

    public override void ForceFill(Coffin coffin)
    {
        // Don't care
    }
}
