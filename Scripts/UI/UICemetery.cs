using Godot;
using System;

public partial class UICemetery : Control
{
    [Export] private TextureRect Background;
    [Export] private Control GravesHolder;
    [Export] private Control Offset;
    
    public void Init(CemeteryData data)
    {
        // TBA pos...
        Background.Texture = data.Background;
        Offset.CustomMinimumSize = Vector2.Down * data.YPositionMod * 2;
    }

    public void AddGrave(Control uiGrave)
    {
        GravesHolder.AddChild(uiGrave);
    }
}
