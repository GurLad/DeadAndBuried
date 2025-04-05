using Godot;
using System;

public partial class UICursor : Control
{
    public static UICursor Current { get; private set; }

    [Export] private UICoffin Renderer { get; set; }

    public Coffin HeldCoffin { get; private set; }
    public UICoffin CoffinHolder { get; private set; }

    [Signal]
    public delegate void OnPickedUpCoffinEventHandler(UICursor cursor, Coffin coffin);

    [Signal]
    public delegate void OnDroppedCoffinEventHandler(UICursor cursor, Coffin coffin);

    [Signal]
    public delegate void OnCancelledCoffinEventHandler(UICursor cursor, Coffin coffin);

    public override void _Ready()
    {
        base._Ready();
        Current = this;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventMouseMotion mouseMotionEvent)
        {
            Position = mouseMotionEvent.Position;
        }
    }

    public void PickUpCoffin(UICoffin coffinHolder, Coffin coffin)
    {
        if (HeldCoffin != null || coffinHolder != null)
        {
            GD.PushError("[UICursor] : Already holding a coffin! Curr: " + HeldCoffin + ", new: " + coffin);
            return;
        }
        HeldCoffin = coffin;
        CoffinHolder = coffinHolder;
        Renderer.Render(coffin);
        Renderer.Visible = true;
        EmitSignal(SignalName.OnPickedUpCoffin, coffin);
    }

    public void DropCoffin(Coffin coffin, AGrave grave)
    {
        if (HeldCoffin != coffin)
        {
            GD.PushError("[UICursor] : Dropping wrong coffin! Curr: " + HeldCoffin + ", new: " + coffin);
            return;
        }
        grave.Fill(coffin);
        CoffinHolder.Remove();
        Renderer.Visible = false;
        EmitSignal(SignalName.OnDroppedCoffin, coffin);
        HeldCoffin = null;
        CoffinHolder = null;
    }

    public void CancelPickUp()
    {
        if (HeldCoffin != null)
        {
            GD.PushError("[UICursor] : Canceling no coffin!");
            return;
        }
        CoffinHolder.CancelDrop();
        Renderer.Visible = false;
        EmitSignal(SignalName.OnCancelledCoffin, HeldCoffin);
        HeldCoffin = null;
        CoffinHolder = null;
    }
}
