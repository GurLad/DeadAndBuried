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
            location: Vector2I.Zero
        ),
        new CemeteryData
        (
            type: GraveType.Underground,
            location: Vector2I.Down
        ),
        new CemeteryData
        (
            type: GraveType.Mass,
            location: Vector2I.Right
        ),
        new CemeteryData
        (
            type: GraveType.Burn,
            location: Vector2I.Right
        ),
        new CemeteryData
        (
            type: GraveType.Spaceship,
            location: Vector2I.Right
        ),
    };
}
