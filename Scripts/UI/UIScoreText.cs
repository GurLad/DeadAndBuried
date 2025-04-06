using Godot;
using System;

public partial class UIScoreText : Label
{
    [Export] private Label DisplayLabel;
    [Export] private float UpSpeed;
    [Export] private float DisplayTime;
    [Export] private float FadeTime;
    [Export] private Color OutlineColor;
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
        if (DisplayLabel.LabelSettings == null)
        {
            DisplayLabel.LabelSettings = new LabelSettings();
            DisplayLabel.LabelSettings.OutlineSize = 2;
            DisplayLabel.LabelSettings.OutlineColor = OutlineColor;
        }
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
                    a => DisplayLabel.LabelSettings.FontColor = new Color(a, a, a, a),
                    1,
                    0),
                new Interpolator.InterpolateObject(
                    a => DisplayLabel.LabelSettings.OutlineColor = new Color(a, a, a, a),
                    1,
                    0));
            interpolator.OnFinish = () => QueueFree();
        };
        GD.Print("Gdffdsfsd");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        DisplayLabel.Position += Vector2.Up * UpSpeed * (float)delta;
    }
}
