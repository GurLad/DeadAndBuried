using Godot;
using System;
using System.Collections.Generic;

public partial class CemeteryLoader : AGameLoader<CemeteryLoader, CemeteryData>
{
    protected List<CemeteryData> CemeteryDatas { get; } = new List<CemeteryData>()
    {
        new CemeteryData
        (
            type: GraveType.Single,
            location: Vector2I.Zero,
            yPositionMod: 34
        ),
        new CemeteryData
        (
            type: GraveType.Underground,
            location: Vector2I.Down,
            yPositionMod: 34
        ),
        new CemeteryData
        (
            type: GraveType.Mass,
            location: Vector2I.Left,
            yPositionMod: 0
        ),
        new CemeteryData
        (
            type: GraveType.Burn,
            location: Vector2I.Right,
            yPositionMod: 0
        ),
        new CemeteryData
        (
            type: GraveType.Spaceship,
            location: Vector2I.Right,
            yPositionMod: 0
        ),
    };
}
