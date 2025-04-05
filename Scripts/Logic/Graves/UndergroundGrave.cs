using Godot;
using System;

public partial class UndergroundGrave : AGrave
{
    public override GraveType Type => GraveType.Underground;
    public SingleGrave OvergroundGrave;

    public override bool CanFill(Coffin coffin)
    {
        return OvergroundGrave.Data.IsEmpty && Data.IsEmpty && Data.IsCompatible(coffin.Data);
    }

    public override int FillAndScore(Coffin coffin)
    {
        return Mathf.RoundToInt(OvergroundGrave.FillAndScore(coffin) * (float)Data.ScoreMultiplier / OvergroundGrave.Data.ScoreMultiplier);
    }
}
