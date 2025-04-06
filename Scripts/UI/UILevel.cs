using Godot;
using System;
using System.Collections.Generic;

public partial class UILevel : Control
{
    [ExportCategory("UI")]
    [Export] public UIScoreDisplay ScoreDisplay;
    [Export] public Button NextLevelButton;
    [ExportCategory("Game")]
    [Export] private Control CoffinHolder;
    [Export] private Control CemeteryHolder;
    [ExportGroup("Scenes")]
    [Export] private PackedScene SceneUICoffin;
    [Export] private PackedScene SceneUICemetery;
    [ExportSubgroup("Graves")]
    [Export] private PackedScene SceneUIFakeGrave;
    [Export] private PackedScene SceneUISingleGrave;
    [Export] private PackedScene SceneUIUndergroundGrave;
    [Export] private PackedScene SceneUIMassGrave;

    public void Init(LevelGeneratorData level, List<AGrave> Graves, List<Coffin> Coffins)
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
            UICemetery cemetery = InitCemetery<SingleGrave>(GraveType.Single);
            FillCemeteryGrid(level, cemetery, GraveType.Single, singleGraves);
        }
        List<UndergroundGrave> undergroundGraves = Graves.ConvertAll(a => a is UndergroundGrave grave ? grave : null).FindAll(a => a != null);
        if (undergroundGraves.Count > 0)
        {
            UICemetery cemetery = InitCemetery<UndergroundGrave>(GraveType.Single);
            FillCemeteryGrid(level, cemetery, GraveType.Underground, undergroundGraves);
        }
        List<MassGrave> massGraves = Graves.ConvertAll(a => a is MassGrave grave ? grave : null).FindAll(a => a != null);
        if (massGraves.Count > 0 && massGraves.Count <= 1)
        {
            UICemetery cemetery = InitCemetery<MassGrave>(GraveType.Single);
            FillCemetery(cemetery, massGraves[0].Type, massGraves);
        }
        else if (massGraves.Count > 0)
        {
            GD.PushError("[UILevel]: Cannot handle more than 1 mass grave!");
        }
    }

    private UICemetery InitCemetery<GraveClass>(GraveType type) where GraveClass : AGrave
    {
        UICemetery cemetery = SceneUICemetery.Instantiate<UICemetery>();
        cemetery.Init(CemeteryLoader.GetRandom(a => a.Type == type));
        CemeteryHolder.AddChild(cemetery);
        return cemetery;
    }

    private void FillCemeteryGrid<GraveClass>(LevelGeneratorData level, UICemetery cemetery, GraveType type, List<GraveClass> graves) where GraveClass : AGrave
    {
        int i = 0;
        for (int y = 0; y < level.SingleGravesGridSize.Y; y++)
        {
            for (int x = 0; x < level.SingleGravesGridSize.X; x++)
            {
                Vector2I index = new Vector2I(x, y);
                if (level.SingleGravesBlacklist.Contains(index))
                {
                    cemetery.AddGrave(SceneUIFakeGrave.Instantiate<Control>());
                    continue;
                }
                if (i > graves.Count)
                {
                    GD.PushError("[UILevel]: Too many grave!");
                    return;
                }
                AddGraveToCemetery(cemetery, type, graves[i++]);
            }
        }
    }

    private void FillCemetery<GraveClass>(UICemetery cemetery, GraveType type, List<GraveClass> graves) where GraveClass : AGrave
    {
        graves.ForEach(a =>
        {
            AddGraveToCemetery(cemetery, type, a);
        });
    }

    private void AddGraveToCemetery<GraveClass>(UICemetery cemetery, GraveType type, GraveClass grave) where GraveClass : AGrave
    {
        AUIGrave uiGrave;
        switch (type)
        {
            case GraveType.Single:
                uiGrave = CreateAndInitUIGrave<SingleGrave, UISingleGrave>(grave is SingleGrave s ? s : null);
                break;
            case GraveType.Underground:
                uiGrave = CreateAndInitUIGrave<UndergroundGrave, UIUndergroundGrave>(grave is UndergroundGrave u ? u : null);
                break;
            default:
                uiGrave = CreateAndInitUIGrave<MassGrave, UIMassGrave>(grave is MassGrave m ? m : null);
                break;
        }
        cemetery.AddGrave(uiGrave);
    }

    private UIGraveClass CreateAndInitUIGrave<GraveClass, UIGraveClass>(GraveClass grave) where GraveClass : AGrave where UIGraveClass : AUIGraveTyped<GraveClass>
    {
        if (grave == null)
        {
            GD.PushError("[UILevel]: Messed up grave!");
            GetTree().Quit();
            return null;
        }
        UIGraveClass uiGrave = (grave.Type switch
        {
            GraveType.Single => SceneUISingleGrave,
            GraveType.Underground => SceneUIUndergroundGrave,
            _ => SceneUIMassGrave,
        }).Instantiate<UIGraveClass>();
        uiGrave.Init(grave);
        return uiGrave;
    }
}
