using Godot;
using System;

public abstract partial class AGrave : Node
{
    public GraveData Data { get; } = new GraveData();
    public abstract GraveType Type { get; }

    [Signal]
    public delegate void OnFilledEventHandler(AGrave grave, Coffin coffin, int score);

    public void Fill(Coffin coffin)
    {
        if (!CanFill(coffin))
        {
            GD.PushError("[Coffin]: Trying to fill an incompatible grave!");
        }
        EmitSignal(SignalName.OnFilled, this, coffin, FillAndScore(coffin));
    }

    public abstract bool CanFill(Coffin coffin);
    public abstract int FillAndScore(Coffin coffin);
}
