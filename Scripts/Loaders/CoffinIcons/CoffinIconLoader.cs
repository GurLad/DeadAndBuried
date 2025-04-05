using Godot;
using System;
using System.Collections.Generic;

public partial class CoffinIconLoader : AGameLoader<CoffinIconLoader, (CoffinType Type, int IconID)>
{
    private Dictionary<CoffinType, List<Texture2D>> Icons { get; } = new Dictionary<CoffinType, List<Texture2D>>();

    public override void _Ready()
    {
        base._Ready();
        foreach (var child in GetChildren())
        {
            if (child is CoffinIconLoaderNode node)
            {
                Icons.AddOrSet(node.CoffinType, (Icons.SafeGet(node.CoffinType) ?? new List<Texture2D>()));
                Icons[node.CoffinType].Add(node.Texture);
            }
            else
            {
                GD.PrintErr("[BodyLoader]: Invalid loader node!");
            }
        }
    }

    protected override List<(CoffinType Type, int IconID)> GetAllInternal(Func<(CoffinType Type, int IconID), bool> predicate)
    {
        for (CoffinType i = 0; i < CoffinType.EndMarker; i++)
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

    public static Texture2D IconIDToTexture(CoffinType type, int id) => Instance.Icons[type][id];
}
