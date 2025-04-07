using Godot;
using System;
using System.Collections.Generic;

public partial class SingleGrave : AGrave
{
    public override GraveType Type => GraveType.Single;
    public List<SingleGrave> AdjacentGraves { get; } = new List<SingleGrave>();
    public Vector2I Pos { get; set; }

    [Signal]
    public delegate void OnMatchedEventHandler(SingleGrave grave);

    public override bool CanFill(Coffin coffin)
    {
        return Data.IsEmpty && Data.IsCompatible(coffin.Data);
    }

    public override int FillAndScore(Coffin coffin)
    {
        //GD.Print(">>>>>>>> " + Pos + " near " + string.Join(", ", AdjacentGraves.ConvertAll(a => a.Pos)));
        Data.PersonData = coffin.Data.PersonData;
        List<SingleGrave> matchingAdjacent = AdjacentGraves.FindAll(a => a.Data.PersonData?.FamilyName == Data.PersonData.FamilyName);
        if (matchingAdjacent.Count > 0)
        {
            matchingAdjacent.ForEach(a => a.EmitSignal(SignalName.OnMatched, a));
            EmitSignal(SignalName.OnMatched, this);
            SoundController.Current.PlaySFX("Match");
            return (1 + matchingAdjacent.Count) * Data.ScoreMultiplier;
        }
        else
        {
            return Data.ScoreMultiplier;
        }
    }
}
