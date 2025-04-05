using Godot;
using System;
using System.Collections.Generic;

public partial class SingleGrave : AGrave
{
    public List<SingleGrave> AdjacentGraves { get; } = new List<SingleGrave>();

    public override bool CanFill(Coffin coffin)
    {
        return !Data.IsEmpty && Data.IsCompatible(coffin.Data);
    }

    public override void Fill(Coffin coffin)
    {
        if (!CanFill(coffin))
        {
            GD.PushError("[Coffin]: Trying to fill an incompatible grave!");
        }
        Data.PersonData = coffin.Data.PersonData;
        EmitSignal(SignalName.OnFilled, this, coffin);
    }
}
