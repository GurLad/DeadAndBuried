using Godot;
using System;

public abstract partial class AGrave : Node
{
    public GraveData Data { get; } = new GraveData();

    [Signal]
    public delegate void OnFilledEventHandler(AGrave grave, Coffin coffin);

    public abstract bool CanFill(Coffin coffin);
    public abstract void Fill(Coffin coffin);
}
