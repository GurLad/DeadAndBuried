using Godot;
using System;
using System.Collections.Generic;

public partial class GraveIconLoader : AGameLoader<GraveIconLoader, (GraveType Type, int IconID)>
{
    private Dictionary<GraveType, List<Texture2D>> Icons { get; } = new Dictionary<GraveType, List<Texture2D>>();

    public override void _Ready()
    {
        base._Ready();
        foreach (var child in GetChildren())
        {
            if (child is GraveIconLoaderNode node)
            {
                Icons.AddOrSet(node.GraveType, (Icons.SafeGet(node.GraveType) ?? new List<Texture2D>()));
                Icons[node.GraveType].Add(node.Texture);
            }
            else
            {
                GD.PrintErr("[BodyLoader]: Invalid loader node!");
            }
        }
    }

    protected override List<(GraveType Type, int IconID)> GetAllInternal(Func<(GraveType Type, int IconID), bool> predicate)
    {
        for (GraveType i = 0; i < GraveType.EndMarker; i++)
        {
            if (predicate((i, 0)))
            {
                return Icons.SafeGet(i).ConvertAll((a, j) => (i, j));
            }
        }
        GD.PushError("[GraveIconLoader]: Bad type!");
        GetTree().Quit();
        return null;
    }

    public static Texture2D IconIDToTexture(GraveType type, int id) => Instance.Icons[type][id];
}
