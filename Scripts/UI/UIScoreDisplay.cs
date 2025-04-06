using Godot;
using System;

public partial class UIScoreDisplay : Label
{
    [Export] private float DisplayTime;

    private Interpolator interpolator;
    private float currentValue = 0;

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator = new Interpolator());
    }

    public void SetScore(int score, bool animate = true)
    {
        if (!animate)
        {
            Text = (currentValue = score).ToString();
        }
        else
        {
            float temp = currentValue;
            interpolator.Interpolate(DisplayTime,
                new Interpolator.InterpolateObject(
                    a => Text = Mathf.RoundToInt(currentValue = a).ToString(),
                    temp,
                    score));
        }
    }
}
