using Godot;
using System;

public abstract partial class AGrave : Node
{
    public GraveData Data { get; } = new GraveData();

    [Signal]
    public delegate void OnFilledEventHandler(AGrave grave, Coffin coffin);

    public void Fill(Coffin coffin)
    {
        if (!CanFill(coffin))
        {
            GD.PushError("[Coffin]: Trying to fill an incompatible grave!");
        }
        ForceFill(coffin);
        EmitSignal(SignalName.OnFilled, this, coffin);
    }

    public abstract bool CanFill(Coffin coffin);
    public abstract void ForceFill(Coffin coffin);
}
