using Godot;
using System;

public partial class UISingleGrave : AUIGraveTyped<SingleGrave>
{
    public override void Init(SingleGrave grave)
    {
        base.Init(grave);
        Grave.OnMatched += RenderMatched;
    }

    private void RenderMatched(SingleGrave grave)
    {
        // TBA...
    }
}
