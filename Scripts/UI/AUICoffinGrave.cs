using Godot;
using System;

public abstract partial class AUICoffinGrave : Control
{
    [ExportCategory("Tooltip")]
    [Export] private bool Upright = true;

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
            UITooltipController.Current.ShowTooltip(this, GetPersonData, Upright);
        }
    }

    protected virtual void OnMouseExited()
    {
        UITooltipController.Current.HideTooltip();
    }
}
