using Godot;
using System;

public abstract partial class AUICoffinGrave : Control
{
    [ExportCategory("Tooltip")]
    [Export] private UITooltip Tooltip;
    [Export] private float TooltipDelayTime = 0.1f;

    protected abstract PersonData GetPersonData { get; }

    public override void _Ready()
    {
        base._Ready();
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
    }

    protected virtual void OnMouseEntered()
    {
        if (GetPersonData != null)
        {
            Tooltip.ShowTooltip(GetPersonData, TooltipDelayTime);
        }
    }

    protected virtual void OnMouseExited()
    {
        Tooltip.HideTooltip();
    }
}
