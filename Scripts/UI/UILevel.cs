using Godot;
using System;
using System.Collections.Generic;

public partial class UILevel : Control
{
    [Export] private Control CoffinHolder;
    [Export] private Control CemeteryHolder;
    [ExportGroup("Scenes")]
    [Export] private PackedScene SceneUICoffin;
    [Export] private PackedScene SceneUICemetery;
    [ExportSubgroup("Graves")]
    [Export] private PackedScene SceneUISingleGrave;
    [Export] private PackedScene SceneUIUndergroundGrave;
    [Export] private PackedScene SceneUIMassGrave;

    public void Init(List<AGrave> Graves, List<Coffin> Coffins)
    {
        Coffins.ForEach(a =>
        {
            UICoffin uiCoffin = SceneUICoffin.Instantiate<UICoffin>();
            uiCoffin.Init(a);
            CoffinHolder.AddChild(uiCoffin);
        });
        List<SingleGrave> singleGraves = Graves.ConvertAll(a => a is SingleGrave grave ? grave : null).FindAll(a => a != null);
        if (singleGraves.Count > 0)
        {
            FillCemetery(GraveType.Single, singleGraves);
        }
        List<UndergroundGrave> undergroundGraves = Graves.ConvertAll(a => a is UndergroundGrave grave ? grave : null).FindAll(a => a != null);
        if (undergroundGraves.Count > 0)
        {
            FillCemetery(GraveType.Underground, undergroundGraves);
        }
        List<MassGrave> massGraves = Graves.ConvertAll(a => a is MassGrave grave ? grave : null).FindAll(a => a != null);
        if (massGraves.Count > 0 && massGraves.Count <= 1)
        {
            FillCemetery(massGraves[0].Type, massGraves);
        }
        else if (massGraves.Count > 0)
        {
            GD.PushError("[UILevel]: Cannot handle more than 1 mass grave!");
        }
    }

    private void FillCemetery<GraveClass>(GraveType type, List<GraveClass> graves) where GraveClass : AGrave
    {
        UICemetery cemetery = SceneUICemetery.Instantiate<UICemetery>();
        cemetery.Init(CemeteryLoader.GetRandom(a => a.Type == type));
        CemeteryHolder.AddChild(cemetery);
        graves.ForEach(a =>
        {
            cemetery.AddChild(a);
        });
    }
}
