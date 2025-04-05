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

    public override int FillAndScore(Coffin coffin)
    {
        return Data.ScoreMultiplier;
    }

    public void SetType(GraveType type)
    {
        _type = type;
    }
}
