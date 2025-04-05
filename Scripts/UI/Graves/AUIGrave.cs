using Godot;
using System;

public abstract partial class AUIGrave : Control {}

public abstract partial class AUIGraveTyped<GraveClass> : AUIGrave where GraveClass : AGrave
{
    private enum HighlightMode
    {
        None = 0,
        CanDrop = 1,
        Hover = 2
    }

    [Export] private TextureRect Icon;
    [ExportCategory("Highlight")]
    [Export] private Color CanDropOutline { get; set; }
    [Export] private Color HoverOutline { get; set; }

    public GraveClass Grave { get; private set; }

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
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        Icon.Material = (Material)Icon.Material.Duplicate();
        ShaderMaterial = Icon.Material is ShaderMaterial sm ? sm : null;
        if (ShaderMaterial == null)
        {
            GD.PushError("[UICoffin]: No shader material!");
            GetTree().Quit();
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
        ShaderMaterial.Set("showOutline", Highlight != HighlightMode.None);
        ShaderMaterial.Set("outlineColor", (Highlight & HighlightMode.Hover) != HighlightMode.None ? HoverOutline : CanDropOutline);
    }

    private void RenderFilled(Coffin coffin, int score)
    {
        // TBA
        Render(Grave);
    }

    private void CursorPickedUpCoffin(UICursor cursor, Coffin coffin)
    {
        if (Grave.CanFill(cursor.HeldCoffin))
        {
            Highlight |= HighlightMode.CanDrop;
        }
    }

    private void CursorDroppedCoffin(UICursor cursor, Coffin coffin)
    {
        Highlight = HighlightMode.None;
    }

    private void OnMouseEntered()
    {
        if (UICursor.Current.HeldCoffin != null && Grave.CanFill(UICursor.Current.HeldCoffin))
        {
            Highlight |= HighlightMode.Hover;
            RenderHighlight();
        }
    }

    private void OnMouseExited()
    {
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
