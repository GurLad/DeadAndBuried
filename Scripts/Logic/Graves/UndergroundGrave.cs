using Godot;
using System;

public partial class UndergroundGrave : AGrave
{
    public SingleGrave OvergroundGrave;

    public override bool CanFill(Coffin coffin)
    {
        return OvergroundGrave.Data.IsEmpty && Data.IsEmpty && Data.IsCompatible(coffin.Data);
    }

    public override void ForceFill(Coffin coffin)
    {
        OvergroundGrave.ForceFill(coffin);
    }
}
