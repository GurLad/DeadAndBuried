using Godot;
using System;

public partial class UICemetery : Control
{
    [Export] private TextureRect Background;
    [Export] private Control GravesHolder;
    [Export] private Control Offset;

    public CemeteryData Data { get; private set; }
    private Interpolator Interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        AddChild(Interpolator);
    }
    
    public void Init(CemeteryData data)
    {
        Data = data;
        Background.Texture = data.Background;
        Offset.CustomMinimumSize = Vector2.Down * data.YPositionMod * 2;
        if (data.Location != Vector2I.Zero)
        {
            Visible = GravesHolder.Visible = false;
        }
    }

    public void AddGrave(Control uiGrave)
    {
        GravesHolder.AddChild(uiGrave);
    }

    public void TransitionIn(UICemetery previous, float time, int dist, Action onFinished)
    {
        Visible = true;
        GravesHolder.Visible = false;
        ZIndex = previous.ZIndex + 1;
        Vector2I basePos = (previous.Data.Location - Data.Location) * dist;
        Interpolator.Interpolate(time,
            new Interpolator.InterpolateObject(
                a => Position = a,
                basePos,
                Vector2.Zero,
                Easing.EaseInBack),
            new Interpolator.InterpolateObject(
                a => Modulate = new Color(Modulate, a),
                0,
                1,
                Easing.EaseInBack));
        Interpolator.OnFinish = () =>
        {
            GravesHolder.Visible = true;
            previous.Visible = previous.GravesHolder.Visible = false;
            onFinished?.Invoke();
        };
    }
}
