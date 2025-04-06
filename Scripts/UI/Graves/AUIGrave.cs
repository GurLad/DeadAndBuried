using Godot;
using System;

public abstract partial class AUIGrave : AUICoffinGrave {}

public abstract partial class AUIGraveTyped<GraveClass> : AUIGrave where GraveClass : AGrave
{
    private enum HighlightMode
    {
        None = 0,
        CanDrop = 1,
        Hover = 2,
        Empty = 4,
        CanDropHover = CanDrop | Hover
    }

    [Export] private TextureRect Icon;
    [ExportCategory("Highlight")]
    [Export] private Color EmptyOutline { get; set; }
    [Export] private Color CanDropOutline { get; set; }
    [Export] private Color HoverOutline { get; set; }

    public GraveClass Grave { get; private set; }

    protected override PersonData GetPersonData => Grave?.Data?.PersonData;

    private HighlightMode Highlight;
    private ShaderMaterial ShaderMaterial;

    public virtual void Init(GraveClass grave)
    {
        Grave = grave;
        Grave.OnFilled += (grave, coffin, score) => RenderFilled(coffin, score);
        AddChild(Grave);
        UICursor.Current.OnPickedUpCoffin += CursorPickedUpCoffin;
        UICursor.Current.OnDroppedCoffin += CursorDroppedCoffin;
        UICursor.Current.OnCancelledCoffin += CursorDroppedCoffin;
        Icon.Material = (Material)Icon.Material.Duplicate();
        ShaderMaterial = Icon.Material is ShaderMaterial sm ? sm : null;
        if (ShaderMaterial == null)
        {
            GD.PushError("[UICoffin]: No shader material!");
            GetTree().Quit();
        }
        if (grave.Data.IsEmpty)
        {
            Highlight |= HighlightMode.Empty;
        }
        Render(Grave);
    }

    private void Render(GraveClass grave)
    {
        GraveType type = grave.Data.IsEmpty ? grave.Type : (grave.Type | GraveType.Filled);
        int iconID = grave.Data.IsEmpty ? GraveIconLoader.GetRandom(a => a.Type == type).IconID : grave.Data.FilledIconID;
        if (iconID >= 0)
        {
            Icon.Texture = GraveIconLoader.IconIDToTexture(type, iconID);
        }
        else
        {
            GD.PushError("[AUIGrave]: No icon for " + type + "!");
        }
        RenderHighlight();
    }

    public void RenderHighlight()
    {
        ShaderMaterial.SetShaderParameter("showOutline", Highlight != HighlightMode.None);
        ShaderMaterial.SetShaderParameter("outlineColor",
            (Highlight & HighlightMode.Hover) != HighlightMode.None ?
                HoverOutline :
                ((Highlight & HighlightMode.CanDrop) != HighlightMode.None ?
                    CanDropOutline :
                    EmptyOutline));
    }

    private void RenderFilled(Coffin coffin, int score)
    {
        UIVFXController.Current.DisplayScore(this, score);
        Render(Grave);
    }

    private void CursorPickedUpCoffin(UICursor cursor, Coffin coffin)
    {
        if (Grave.CanFill(cursor.HeldCoffin))
        {
            Highlight |= HighlightMode.CanDrop;
            RenderHighlight();
        }
    }

    private void CursorDroppedCoffin(UICursor cursor, Coffin coffin)
    {
        Highlight &= ~HighlightMode.CanDropHover;
        RenderHighlight();
    }

    protected override void OnMouseEntered()
    {
        base.OnMouseEntered();
        if (UICursor.Current.HeldCoffin != null && Grave.CanFill(UICursor.Current.HeldCoffin))
        {
            Highlight |= HighlightMode.Hover;
            RenderHighlight();
        }
    }

    protected override void OnMouseExited()
    {
        base.OnMouseExited();
        Highlight &= ~HighlightMode.Hover;
        RenderHighlight();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        UICursor.Current.OnPickedUpCoffin -= CursorPickedUpCoffin;
        UICursor.Current.OnDroppedCoffin -= CursorDroppedCoffin;
        UICursor.Current.OnCancelledCoffin -= CursorDroppedCoffin;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (data.As<GodotObject>() is UICursor cursor && cursor.HeldCoffin != null && Grave.CanFill(cursor.HeldCoffin))
        {
            return true;
        }
        return false;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        if (data.As<GodotObject>() is UICursor cursor)
        {
            if (cursor.HeldCoffin != null && Grave.CanFill(cursor.HeldCoffin))
            {
                cursor.DropCoffin(cursor.HeldCoffin, Grave);
                Highlight &= ~HighlightMode.Empty;
            }
            else
            {
                GD.PushError("[AUIGrave]: Placing invalid coffin!");
            }
        }
        else
        {
            GD.PushError("[AUIGrave]: Placing non-coffins!");
        }
    }
}
