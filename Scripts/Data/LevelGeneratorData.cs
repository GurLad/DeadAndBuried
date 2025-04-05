using Godot;
using System;
using System.Collections.Generic;

public partial class LevelGeneratorData : Node
{
    // Coffins
    public int TotalCoffinCount { get; } = 0;
    public int CopyNewFamilyCoffinCount { get; } = 0; // Each one here copies the family name from another coffing here
    public int CopyOldFamilyCoffinCount { get; } = 0; // Each one here copies the family name from an old coffin
    public List<PersonData> HardcodedCoffins = new List<PersonData>();
    // Graves
    public Vector2ISerializable SingleGravesGridSize { get; } = new Vector2ISerializable(1, 1);
    public List<Vector2I> SingleGravesBlacklist { get; } = new List<Vector2I>();
    public Dictionary<GraveType, CoffinType> GraveCompatibleTypes { get; } = new Dictionary<GraveType, CoffinType>()
    {
        { GraveType.Single, CoffinType.All },
        { GraveType.Underground, CoffinType.All },
        { GraveType.Mass, CoffinType.All },
    };
}
