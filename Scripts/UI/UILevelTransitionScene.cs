using Godot;
using System;

public partial class UILevelTransitionScene : FadeTransition
{
    [Export] private Label NightLabel;
    [Export] private float HoldTime;
    [Export] private float DelayTime;

    public override void _Ready()
    {
        base._Ready();
        NightLabel.Text = "NIGHT " + (Level.SaveData.CurrentLevel + 1);
        Interpolator interpolator = new Interpolator();
        AddChild(interpolator);
        interpolator.Delay(DelayTime);
        interpolator.OnFinish = () =>
        {
            TransitionIn(() =>
            {
                SoundController.Current.PlaySFX("NextNight");
                interpolator.Delay(HoldTime);
                interpolator.OnFinish = () =>
                {
                    TransitionOut(() => SceneController.Current.TransitionToScene("Level"));
                };
            });
        };
    }
}
