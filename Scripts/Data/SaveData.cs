using Godot;
using System;
using System.Collections.Generic;

public class SaveData
{
    public int CurrentLevel { get; set; } = 5;
    public int Score { get; set; } = 0;
    public Dictionary<Vector2I, GraveData> Graves { get; } = new Dictionary<Vector2I, GraveData>();
}
