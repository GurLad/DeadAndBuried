using Godot;
using System;

public partial class UIScoreText : Control
{
    [Export] private Label DisplayLabel;
    [Export] private float UpSpeed;
    [Export] private float DisplayTime;
    [Export] private float FadeTime;
    [Export] private Godot.Collections.Dictionary<int, Font> FontThresolds;
    [Export] private Godot.Collections.Dictionary<int, Color> ColorThresolds;
    
    private Interpolator interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
    }

    public void Display(int amount, Vector2 pos)
    {
        Visible = true;
        bool found = false;
        foreach (var item in ColorThresolds)
        {
            if (amount > item.Key || !found)
            {
                DisplayLabel.LabelSettings.FontColor = item.Value;
                found = true;
            }
        }
        found = false;
        foreach (var item in FontThresolds)
        {
            if (amount > item.Key || !found)
            {
                DisplayLabel.LabelSettings.Font = item.Value;
                found = true;
            }
        }
        DisplayLabel.Position = pos;
        DisplayLabel.Text = amount.ToString();
        interpolator.Delay(DisplayTime);
        interpolator.OnFinish = () =>
        {
            interpolator.Interpolate(FadeTime,
                new Interpolator.InterpolateObject(
                    a => DisplayLabel.AddThemeColorOverride("font_color", new Color(a, a, a, a)),
                    1,
                    0),
                new Interpolator.InterpolateObject(
                    a => DisplayLabel.AddThemeColorOverride("font_outline_color", new Color(a, a, a, a)),
                    1,
                    0));
            interpolator.OnFinish = () => QueueFree();
        };
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        DisplayLabel.Position += Vector2.Up * UpSpeed * (float)delta;
    }
}
