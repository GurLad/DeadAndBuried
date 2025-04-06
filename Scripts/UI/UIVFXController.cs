using Godot;
using System;
using System.Collections.Generic;

public partial class UIVFXController : Node
{
    public static UIVFXController Current { get; private set; }

    [Export] private PackedScene SceneScoreText;

    public override void _Ready()
    {
        base._Ready();
        Current = this;
    }

    public void DisplayScore(Control control, int amount)
    {
        UIScoreText damageText = SceneScoreText.Instantiate<UIScoreText>();
        AddChild(damageText);
        Vector2 pos = control.GlobalPosition;// + damageText.Size / 2;
        damageText.Display(amount, pos);
        damageText.Position = pos + new Vector2(ExtensionMethods.RNG.Next(-5, 6), ExtensionMethods.RNG.Next(-5, 6));
    }
}
