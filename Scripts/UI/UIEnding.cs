using Godot;
using System;

public partial class UIEnding : Node
{
    [Export] private Label label;

    public override void _Ready()
    {
        base._Ready();
        label.Text = string.Format(label.Text, Level.SaveData.Score);
    }
}
