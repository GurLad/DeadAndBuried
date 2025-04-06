using Godot;
using System;

public partial class UITooltipController : Node
{
    public static UITooltipController Current { get; private set; }

    [Export] private UITooltip TooltipUpright;
    [Export] private UITooltip TooltipSideways;
    [Export] private float delay = 0.1f;

    public override void _Ready()
    {
        base._Ready();
        Current = this;
    }

    public void ShowTooltip(Control source, PersonData personData, bool upright)
    {
        UITooltip tooltip = upright ? TooltipUpright : TooltipSideways;
        tooltip.GlobalPosition = source.GlobalPosition + tooltip.Size / 2 - source.Size -
            (upright ?
                tooltip.Size.X * Vector2.Right / 2 :
                tooltip.Size.Y * Vector2.Up / 2);
        tooltip.ShowTooltip(personData, delay);
    }

    public void HideTooltip()
    {
        TooltipUpright.HideTooltip();
        TooltipSideways.HideTooltip();
    }
}
