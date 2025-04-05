using Godot;
using System;

public partial class UICemetery : Control
{
    [Export] private Sprite2D Background;
    [Export] private Control GravesHolder;
    
    public void Init(CemeteryData data)
    {
        // TBA pos...
        Background.Texture = data.Background;
    }

    public void AddGrave(AUIGrave uiGrave)
    {
        AddChild(uiGrave);
    }
}
