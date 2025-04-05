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

    public GraveClass Grave { get; private set; }

    private HighlightMode Highlight;

    public virtual void Init(GraveClass grave)
    {
        Grave = grave;
        Grave.OnFilled += (grave, coffin, score) => RenderFilled(coffin, score);
        AddChild(Grave);
        Render(Grave);
        UICursor.Current.OnPickedUpCoffin += CursorPickedUpCoffin;
        UICursor.Current.OnDroppedCoffin += CursorDroppedCoffin;
        UICursor.Current.OnCancelledCoffin += CursorDroppedCoffin;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
    }

    private void Render(GraveClass grave)
    {
        GraveType type = grave.Data.IsEmpty ? grave.Type : (grave.Type | GraveType.Filled);
        Icon.Texture = GraveIconLoader.IconIDToTexture(type, grave.Data.IsEmpty ? GraveIconLoader.GetRandom(a => a.Type == type).IconID : grave.Data.FilledIconID);
        RenderHighlight();
    }

    public void RenderHighlight()
    {
        // TBA
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
