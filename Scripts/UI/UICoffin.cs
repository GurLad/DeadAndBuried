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
    [Export] private TextureRect Icon;
    [ExportCategory("Highlight")]
    [Export] private float HeldOpacity { get; set; } = 0.6f;
    [Export] private Color HoverOutline { get; set; }

    public Coffin Coffin { get; private set; }

    private HighlightMode Highlight;
    private ShaderMaterial ShaderMaterial;

    [Signal]
    public delegate void OnRemovedEventHandler(UICursor cursor, Coffin coffin);

    public virtual void Init(Coffin coffin)
    {
        Coffin = coffin;
        AddChild(Coffin);
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        Icon.Material = (Material)Icon.Material.Duplicate();
        ShaderMaterial = Icon.Material is ShaderMaterial sm ? sm : null;
        if (ShaderMaterial == null)
        {
            GD.PushError("[UICoffin]: No shader material!");
            GetTree().Quit();
        }
        ShaderMaterial.Set("outlineColor", HoverOutline);
        Render(Coffin);
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
        int iconID = coffin.Data.IconID;
        if (iconID >= 0)
        {
            Icon.Texture = CoffinIconLoader.IconIDToTexture(coffin.Data.Types, iconID);
        }
        else
        {
            GD.PushError("[AUIGrave]: No icon for " + coffin.Data.Types + "!");
        }
        RenderHighlight();
    }

    public void RenderHighlight()
    {
        if (!CanDrag)
        {
            return;
        }
        Modulate = new Color(Modulate, (Highlight & HighlightMode.Held) != HighlightMode.None ? HeldOpacity : 1);
        ShaderMaterial.Set("showOutline", (Highlight & HighlightMode.Hover) != HighlightMode.None);
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
        Highlight |= HighlightMode.Held;
        RenderHighlight();
        UICursor.Current.PickUpCoffin(this, Coffin);
        return UICursor.Current;
    }
}
