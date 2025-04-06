using Godot;
using System;

public partial class UIMatchIndicator : FadeTransition
{
    [Export] private float HoldTime;
    [Export] private float DelayTime;

    public void Display()
    {
        interpolator.Delay(DelayTime);
        interpolator.OnFinish = () =>
        {
            TransitionIn(() =>
            {
                interpolator.Delay(HoldTime);
                interpolator.OnFinish = () =>
                {
                    TransitionOut();
                };
            });
        };
    }
}
