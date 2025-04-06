using Godot;
using System;
using System.Collections.Generic;

public partial class UIVFXController : Node
{
    public static UIVFXController Current { get; private set; }

    [Export] private PackedScene SceneScoreText;
    [Export] private PackedScene SceneMatchIndicator;

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

    public void DisplayMatchVFX(Control control)
    {
        UIMatchIndicator matchIcon = SceneMatchIndicator.Instantiate<UIMatchIndicator>();
        AddChild(matchIcon);
        Vector2 pos = control.GlobalPosition + Vector2.Up * (matchIcon.Size.Y / 2 - 32); // whatevs
        matchIcon.Display();
        matchIcon.Position = pos + new Vector2(ExtensionMethods.RNG.Next(-5, 6), ExtensionMethods.RNG.Next(-5, 6));
    }
}
