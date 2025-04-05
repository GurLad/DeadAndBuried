using Godot;
using System;

public partial class GraveIconLoaderNode : Sprite2D
{
    [Export] private GraveType _graveType;
    [Export] private bool Filled;

    public GraveType GraveType => Filled ? (_graveType | GraveType.Filled) : _graveType;
}
