using Godot;
using System;
using System.Collections.Generic;

public partial class SingleGrave : AGrave
{
    public override GraveType Type => throw new NotImplementedException();
    public List<SingleGrave> AdjacentGraves { get; } = new List<SingleGrave>();

    public override bool CanFill(Coffin coffin)
    {
        return Data.IsEmpty && Data.IsCompatible(coffin.Data);
    }

    public override void ForceFill(Coffin coffin)
    {
        Data.PersonData = coffin.Data.PersonData;
    }
}
