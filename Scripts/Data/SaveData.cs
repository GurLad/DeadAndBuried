using Godot;
using System;
using System.Collections.Generic;

public class SaveData
{
    public int CurrentLevel { get; set; } = 0;
    public Dictionary<Vector2ISerializable, GraveData> Graves { get; } = new Dictionary<Vector2ISerializable, GraveData>();
}
