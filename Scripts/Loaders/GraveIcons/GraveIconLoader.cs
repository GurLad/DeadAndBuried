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
                GD.Print("[GraveIconLoader]: Loaded " + node.GraveType);
            }
            else
            {
                GD.PrintErr("[GraveIconLoader]: Invalid loader node!");
            }
        }
    }

    protected override List<(GraveType Type, int IconID)> GetAllInternal(Func<(GraveType Type, int IconID), bool> predicate)
    {
        for (GraveType i = 0; i < GraveType.EndMarker; i++)
        {
            if (predicate((i, 0)))
            {
                return Icons.SafeGet(i)?.ConvertAll((a, j) => (i, j)) ?? new List<(GraveType i, int j)>();
            }
            if (predicate((i | GraveType.Filled, 0)))
            {
                return Icons.SafeGet(i | GraveType.Filled)?.ConvertAll((a, j) => (i | GraveType.Filled, j)) ?? new List<(GraveType i, int j)>();
            }
        }
        GD.PushError("[GraveIconLoader]: Bad type!");
        GetTree().Quit();
        return null;
    }

    protected override (GraveType Type, int IconID) GetRandomInternal(Func<(GraveType Type, int IconID), bool> predicate)
    {
        List<(GraveType Type, int IconID)> options = GetAllInternal(predicate);
        if (options.Count > 0)
        {
            return base.GetRandomInternal(predicate);
        }
        else
        {
            return (GraveType.None, -1);
        }
    }

    public static Texture2D IconIDToTexture(GraveType type, int id) => Instance.Icons[type][id];
}
