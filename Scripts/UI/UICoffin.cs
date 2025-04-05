using Godot;
using System;

public partial class UICoffin : Control
{
    private enum HighlightMode
    {
        None = 0,
        Held = 1,
        Hover = 2
    }

    [Export] private bool CanDrag { get; set; } = true;
    [Export] private Sprite2D Icon;
    [ExportCategory("Highlight")]
    [Export] private float HeldOpacity { get; set; } = 0.6f;

    public Coffin Coffin { get; private set; }

    private HighlightMode Highlight;

    [Signal]
    public delegate void OnRemovedEventHandler(UICursor cursor, Coffin coffin);

    public virtual void Init(Coffin coffin)
    {
        Coffin = coffin;
        AddChild(Coffin);
        Render(Coffin);
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
    }

    public void Remove()
    {
        EmitSignal(SignalName.OnRemoved, Coffin);
        QueueFree();
    }

    public void CancelDrop()
    {
        Highlight &= ~HighlightMode.Held;
    }

    public void Render(Coffin coffin)
    {
        Icon.Texture = CoffinIconLoader.IconIDToTexture(Coffin.Data.Types, CoffinIconLoader.GetRandom(a => a.Type == Coffin.Data.Types).IconID);
        RenderHighlight();
    }

    public void RenderHighlight()
    {
        if (!CanDrag)
        {
            return;
        }
        Modulate = new Color(Modulate, (Highlight & HighlightMode.Held) != HighlightMode.None ? HeldOpacity : 1);
        // TBA
    }

    private void OnMouseEntered()
    {
        Highlight |= HighlightMode.Hover;
        RenderHighlight();
    }

    private void OnMouseExited()
    {
        Highlight &= ~HighlightMode.Hover;
        RenderHighlight();
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (!CanDrag)
        {
            return this;
        }
        if (Coffin == null)
        {
            GD.PushWarning("[UICoffin] : Dragging empty coffin!");
            return this;
        }
        OnMouseExited();
        UICursor.Current.PickUpCoffin(this, Coffin);
        return UICursor.Current;
    }
}
