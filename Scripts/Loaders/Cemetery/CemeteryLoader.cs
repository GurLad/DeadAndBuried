using Godot;
using System;
using System.Collections.Generic;

public partial class CemeteryLoader : AGameLoader<CemeteryLoader, CemeteryData>
{
    public override void _Ready()
    {
        base._Ready();
        foreach (var child in GetChildren())
        {
            if (child is Sprite2D sprite)
            {
                CemeteryData data = CemeteryDatas.Find(a => a.Type switch
                {
                    GraveType.Single => sprite.Name == "Single",
                    GraveType.Underground => sprite.Name == "Underground",
                    GraveType.Mass => sprite.Name == "Mass",
                    GraveType.Burn => sprite.Name == "Burn",
                    GraveType.Spaceship => sprite.Name == "Spaceship",
                    _ => false
                });
                if (data != null)
                {
                    data.Background = sprite.Texture;
                    GD.Print("[CemeteryLoader]: Loaded " + data.Type);
                }
                else
                {
                    GD.PrintErr("[CemeteryLoader]: Sprite without a record! " + sprite.Name);
                }
            }
            else
            {
                GD.PrintErr("[CemeteryLoader]: Invalid sprite!");
            }
        }
    }

    protected override List<CemeteryData> GetAllInternal(Func<CemeteryData, bool> predicate) => CemeteryDatas.FindAll(a => predicate(a));
}
