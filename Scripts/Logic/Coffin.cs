using Godot;
using System;

public partial class Coffin : Node
{
    public CoffinData Data { get; } = new CoffinData();

    public Coffin(CoffinData data)
    {
        Data = data;
    }
}
