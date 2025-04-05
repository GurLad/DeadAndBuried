using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIConversationPlayer : Control
{
    private enum State { Idle, WaitForInput }

    [Export]
    private Godot.Collections.Dictionary<string, Texture2D> portraits;
    [Export]
    private float textSpeed;
    [Export]
    private float showHideTime;
    [Export]
    private float showHideDistance;
    [Export]
    private float lineJumpTime;
    [Export]
    private float lineJumpDistance;
    [Export]
    private TextureRect portraitHolder;
    [Export]
    private Control textContainer;
    [Export]
    private Label text;
    [Export]
    private Control clickBlocker;
    private Interpolator interpolator = new Interpolator();
    private float shownHeight;
    private float hiddenHeight;
    private float lineBaseHeight;
    private float lineJumpHeight;
    private float leftX;
    private float rightX;
    private State state;
    private List<string> lines = new List<string>();
    private string currentLine;

    [Signal]
    public delegate void FinishedConversationEventHandler();

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
        shownHeight = Position.Y;
        hiddenHeight = Position.Y + showHideDistance;
        lineBaseHeight = textContainer.Position.Y;
        lineJumpHeight = textContainer.Position.Y + lineJumpDistance;
        Position = new Vector2(Position.X, hiddenHeight);
        Modulate = new Color(Modulate, 0);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouse)
        {
            if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left)
            {
                if (state == State.WaitForInput)
                {
                    NextLine();
                }
            }
        }
    }

    public void BeginConversation(string content) => BeginConversation(content.Trim().Replace("\r", "").Split("\n").ToList());

    public void BeginConversation(List<string> newLines)
    {
        clickBlocker.Visible = true;
        text.Text = "";
        lines = newLines;
        textContainer.Position = new Vector2(Position.X, lineBaseHeight);
        interpolator.Interpolate(showHideTime,
            new Interpolator.InterpolateObject(
                a => Position = new Vector2(Position.X, a),
                hiddenHeight,
                shownHeight,
                Easing.EaseOutQuad),
            new Interpolator.InterpolateObject(
                a => Modulate = new Color(Modulate, a),
                Modulate.A,
                1,
                Easing.EaseOutQuad));
        interpolator.OnFinish = () => NextLine();
    }

    public void NextLine()
    {
        try
        {
            void TrueBegin()
            {
                text.Text = currentLine;
                state = State.WaitForInput;
                lines.RemoveAt(0);
                interpolator.Interpolate(lineJumpTime / 2,
                    new Interpolator.InterpolateObject(
                    a => textContainer.Position = new Vector2(Position.X, a),
                    textContainer.Position.Y,
                    lineJumpHeight,
                    Easing.EaseOutQuad));
                interpolator.OnFinish = () =>
                {
                    interpolator.Interpolate(lineJumpTime / 2,
                        new Interpolator.InterpolateObject(
                        a => textContainer.Position = new Vector2(Position.X, a),
                        textContainer.Position.Y,
                        lineBaseHeight,
                        Easing.EaseOutQuad));
                };
            }

            state = State.Idle;
            if (lines.Count <= 0)
            {
                HideUI();
                return;
            }
            List<string> parts = lines[0].Split(":").ToList().ConvertAll(a => a.Trim());
            portraitHolder.Texture = portraits[parts[0]];
            currentLine = parts[1].Trim();
            TrueBegin();
        }
        catch
        {
            if (lines.Count > 0)
            {
                GD.PushError("Error in line " + lines[0] + "!");
                lines.RemoveAt(0);
                NextLine();
            }
            else
            {
                GD.PushError("Error no lines!");
                HideUI();
            }
        }
    }

    private void HideUI()
    {
        state = State.Idle;
        textContainer.Position = new Vector2(Position.X, lineBaseHeight);
        interpolator.Interpolate(showHideTime,
            new Interpolator.InterpolateObject(
                a => Position = new Vector2(Position.X, a),
                shownHeight,
                hiddenHeight,
                Easing.EaseInQuad),
            new Interpolator.InterpolateObject(
                a => Modulate = new Color(Modulate, a),
                Modulate.A,
                0,
                Easing.EaseInQuad));
        interpolator.OnFinish = () =>
        {
            clickBlocker.Visible = false;
            text.Text = "";
            EmitSignal(SignalName.FinishedConversation);
        };
    }
}
