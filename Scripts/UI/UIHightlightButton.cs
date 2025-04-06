using Godot;
using System;

public partial class UIHightlightButton : BaseButton
{
    [Export] private float transitionTime = 0.3f;
    [Export] private float rotateTime = 0.4f;

    private Interpolator Interpolator = new Interpolator();
    private ShaderMaterial ShaderMaterial;

    public override void _Ready()
    {
        base._Ready();
        AddChild(Interpolator);
        Material = (Material)Material.Duplicate();
        ShaderMaterial = Material is ShaderMaterial sm ? sm : null;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        PivotOffset = Size / 2 + Vector2.Right * (Size.X / 2 + 5f); // wtf
    }

    protected virtual void OnMouseEntered()
    {
        var color = ShaderMaterial.GetShaderParameter("modulate").As<Color>();
        if (color.A < 1)
        {
            return;
        }
        ShaderMaterial.SetShaderParameter("showOutline", true);
    }

    protected virtual void OnMouseExited()
    {
        ShaderMaterial.SetShaderParameter("showOutline", false);
    }

    public void FakeHide()
    {
        ShaderMaterial.SetShaderParameter("modulate",new Color(Modulate, 0));
    }

    public void TransitionIn(Action midTransition = null)
    {
        Visible = true;
        MouseFilter = MouseFilterEnum.Stop;
        ShaderMaterial.SetShaderParameter("modulate",new Color(Modulate, 0));
        Interpolator.Interpolate(transitionTime, new Interpolator.InterpolateObject(
            a => ShaderMaterial.SetShaderParameter("modulate",new Color(Modulate, a)),
            0,
            1));
        Interpolator.OnFinish = midTransition;
    }

    public void TransitionOut(Action postTransition = null)
    {
        ShaderMaterial.SetShaderParameter("modulate",new Color(Modulate, 1));
        Interpolator.Interpolate(transitionTime, new Interpolator.InterpolateObject(
            a => ShaderMaterial.SetShaderParameter("modulate",new Color(Modulate, a)),
            1,
            0));
        Interpolator.OnFinish = () =>
        {
            MouseFilter = MouseFilterEnum.Ignore;
            Visible = false;
            postTransition?.Invoke();
        };
    }

    public void AnimateRotate(float target, Action postTransition = null, Func<float, float> easing = null)
    {
        Modulate = new Color(Modulate, 1);
        float currRot = Rotation;
        Interpolator.Interpolate(rotateTime, new Interpolator.InterpolateObject(
            a => Rotation = a,
            currRot,
            currRot + target,
            easing));
        Interpolator.OnFinish = () =>
        {
            Rotation = currRot + target;
            postTransition?.Invoke();
        };
    }
}
