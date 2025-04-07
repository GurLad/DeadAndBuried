using Godot;
using System;

public partial class UIQuitButton : UIHightlightButton
{
    public override void _Ready()
    {
        base._Ready();
        Pressed += () => GetTree().Quit();
    }
}
